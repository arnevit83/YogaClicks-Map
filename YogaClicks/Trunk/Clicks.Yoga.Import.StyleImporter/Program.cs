using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Import.StyleImporter
{
    class Style
    {
        public int ImageNumber;
        public string ImagePath;
        public string DirectoryImagePath;
        public string Name;
        public string Founder;
        public string Founded;
        public string About;
        public string SelectorWords;
        public string ImageCourtesy;
        public Guid ImageGuid;
        public Guid DirectoryGuid;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var styles = new List<Style>();
            var filePath = @"C:\Users\SebButcher\Dropbox\Clients\Yoga Clicks\Shared\Content final\Final content for Beta site January 14 2014\Style directory";
            var fileName = Path.Combine(filePath, "Style directory Jan 17 2014.xls");
            var imageStorePath = @"C:\Source Code\Purple Tuesday\YogaClicks\Trunk\Clicks.Yoga.Portal.ImageStore\Images";
            var imagesPath = @"C:\Users\SebButcher\Dropbox\Clients\Yoga Clicks\Shared\Johns designs\Final photos Aug 2013";
            var directoryImages = Path.Combine(imagesPath, "Directory Images");

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", fileName)))
            {
                con.Open();

                using (var cmd = new OleDbCommand("select * from [Sheet1$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Style style = null;

                        while (reader.Read())
                        {
                            style = new Style {
                                ImageNumber = Convert.ToInt32(reader["Image Number ID"]),
                                Name = EnsureString(reader["Style Name"]),
                                Founder = "",//EnsureString(reader["Founder"]),
                                Founded = "",//EnsureString(reader["Founded"]),
                                About = EnsureString(reader["About"]),
                                SelectorWords = EnsureString(reader["Selector Words"]),
                                ImageCourtesy = EnsureString(reader["Image Courtesy"]),
                                ImagePath = Path.Combine(imagesPath, EnsureString(reader["Image Number ID"]) + ".jpg"),
                                DirectoryImagePath = Path.Combine(directoryImages, EnsureString(reader["Image Number ID"]) + ".png")
                            };

                            styles.Add(style);
                        }
                    }
                }
            }

            using (var con = new SqlConnection("server=(local);database=YogaClicksImport;integrated security=true;"))
            {
                con.Open();

                using (var command = new SqlCommand("delete from Style", con))
                {
                    command.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand("insert Style ([Image Number ID], [Style Name], Founder, Founded, Description, [Selector Words], Courtesy, ImageGuid, DirectoryImageGuid) values (@imageId, @name, @founder, @founded, @about, @selectorWords, @courtesy, @imageGuid, @directoryGuid)", con))
                {
                    cmd.Parameters.Add("@imageId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 255);
                    cmd.Parameters.Add("@founder", System.Data.SqlDbType.NVarChar, 255);
                    cmd.Parameters.Add("@founded", System.Data.SqlDbType.NVarChar, 255);
                    cmd.Parameters.Add("@about", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@selectorWords", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@courtesy", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@imageGuid", System.Data.SqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("@directoryGuid", System.Data.SqlDbType.UniqueIdentifier);

                    foreach (var style in styles)
                    {
                        style.ImageGuid = Guid.NewGuid();
                        style.DirectoryGuid = Guid.NewGuid();

                        File.Copy(style.ImagePath, Path.Combine(imageStorePath, "Style.Image", string.Format("{0}.jpg", style.ImageGuid)));
                        File.Copy(style.DirectoryImagePath, Path.Combine(imageStorePath, "Style.DirectoryImage", string.Format("{0}.png", style.DirectoryGuid)));

                        cmd.Parameters["@imageId"].Value = style.ImageNumber;
                        cmd.Parameters["@name"].Value = style.Name;
                        cmd.Parameters["@founder"].Value = style.Founder;
                        cmd.Parameters["@founded"].Value = style.Founded;
                        cmd.Parameters["@about"].Value = style.About;
                        cmd.Parameters["@selectorWords"].Value = style.SelectorWords;
                        cmd.Parameters["@courtesy"].Value = style.ImageCourtesy;
                        cmd.Parameters["@imageGuid"].Value = style.ImageGuid;
                        cmd.Parameters["@directoryGuid"].Value = style.DirectoryGuid;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string EnsureString(object value)
        {
            if (value is DBNull) return "";
            return (string)value;
        }

        private static object GetField(OleDbDataReader reader, params string[] names)
        {
            foreach (var name in names)
            {
                try
                {
                    return reader[name];
                }
                catch (IndexOutOfRangeException)
                {
                }
            }

            throw new IndexOutOfRangeException();
        }
    }
}
