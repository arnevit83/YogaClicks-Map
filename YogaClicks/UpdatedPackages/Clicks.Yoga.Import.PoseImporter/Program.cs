using System;
using System.Collections.Generic;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Clicks.Yoga.Import.PoseImporter
{
    class Category
    {
        public int Id;
        public string Name;
    }

    class Pose
    {
        public Pose()
        {
            Roots = new List<string>();
            Instructions = new List<string>();
            Effects = new List<string>();
            Tips = new List<string>();
            Indications = new List<string>();
            Contraindications = new List<string>();
        }

        public string EnglishName;
        public string SanskritName;
        public List<string> Roots;
        public string Pronounciation;
        public List<string> Instructions;
        public List<string> Effects;
        public List<string> Tips;
        public List<string> Indications;
        public List<string> Contraindications;
        public int AbilityLevelId;
        public string ImagePath;
        public Guid ImageGuid;
        public int CategoryId;
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var rootPath = @"C:\Users\SebButcher\Dropbox\Clients\Yoga Clicks\Shared\Content final\Final content for Beta site January 14 2014\Pose directory";
            var imagesPath = "Updated";
            var textPath = rootPath;
            var imageStorePath = @"C:\Source Code\Purple Tuesday\YogaClicks\Trunk\Clicks.Yoga.Portal.ImageStore\Images\Pose.Image";

            var categories = new Category[]
            {
                new Category {Id = 1, Name = "Standing"},
                new Category {Id = 2, Name = "Weight on hands"},
                new Category {Id = 3, Name = "Kneeling"},
                new Category {Id = 4, Name = "Seated and twists"},
                new Category {Id = 5, Name = "Lying face down"},
                new Category {Id = 6, Name = "Lying face up"}
            };

            var poses = new List<Pose>();

            foreach (var category in categories)
            {
                var directoryPath = Path.Combine(textPath, category.Name);

                foreach (var filePath in Directory.GetFiles(directoryPath))
                {
                    if (!new FileInfo(filePath).Extension.StartsWith(".xls")) continue;

                    using (var connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", filePath)))
                    {
                        connection.Open();

                        using (var command = new OleDbCommand("select * from [Sheet1$]", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                Pose pose = null;

                                while (reader.Read())
                                {
                                    var name = EnsureString(reader["English Name"]);

                                    if (!string.IsNullOrWhiteSpace(name))
                                    {
                                        pose = new Pose
                                        {
                                            EnglishName = name,
                                            SanskritName = EnsureString(reader["Sanskrit Name"]),
                                            Pronounciation = EnsureString(reader["How to say it"]),
                                            AbilityLevelId = Convert.ToInt32(reader["Level"]),
                                            ImagePath = Path.Combine(Path.Combine(rootPath, category.Name), "Updated", EnsureString(reader["Image 1"])),
                                            CategoryId = category.Id
                                        };

                                        if (!File.Exists(pose.ImagePath)) throw new FileNotFoundException(pose.ImagePath);

                                        poses.Add(pose);
                                    }

                                    string value;

                                    value = EnsureString(reader["Root"]);
                                    if (!string.IsNullOrWhiteSpace(value)) pose.Roots.Add(value);

                                    value = EnsureString(reader["How to do it"]);
                                    if (!string.IsNullOrWhiteSpace(value)) pose.Instructions.Add(value);

                                    value = EnsureString(GetField(reader, "Why we love this pose", "Why we love this pose:"));
                                    if (!string.IsNullOrWhiteSpace(value)) pose.Tips.Add(value);
                                    
                                    value = EnsureString(GetField(reader, "Contraindications", "Contraindication", "Bad for", "Bad if you have:"));
                                    if (!string.IsNullOrWhiteSpace(value)) pose.Contraindications.Add(value);

                                    value = EnsureString(GetField(reader, "Good for", "Good if you have:"));
                                    if (!string.IsNullOrWhiteSpace(value)) pose.Indications.Add(value);
                                }
                            }
                        }
                    }
                }
            }

            using (var connection = new SqlConnection("Data Source=(local);Database=YogaClicksImport;Integrated Security=SSPI;"))
            {
                connection.Open();

                using (var command = new SqlCommand("delete from Pose", connection))
                {
                    command.ExecuteNonQuery();
                }

                foreach (var pose in poses)
                {
                    pose.ImageGuid = Guid.NewGuid();

                    File.Copy(pose.ImagePath, Path.Combine(imageStorePath, string.Format("{0}.png", pose.ImageGuid)));

                    using (var command = new SqlCommand("insert into Pose (EnglishName, SanskritName, Roots, Pronounciation, Instructions, Effects, Tips, Indications, Contraindications, AbilityLevelId, ImageGuid, VideoName, CategoryId) values (@EnglishName, @SanskritName, @Roots, @Pronounciation, @Instructions, @Effects, @Tips, @Indications, @Contraindications, @AbilityLevelId, @ImageGuid, @VideoName, @CategoryId)", connection))
                    {
                        command.Parameters.AddWithValue("@EnglishName", pose.EnglishName);
                        command.Parameters.AddWithValue("@SanskritName", pose.SanskritName);
                        command.Parameters.AddWithValue("@Roots", string.Join(Environment.NewLine, pose.Roots));
                        command.Parameters.AddWithValue("@Pronounciation", pose.Pronounciation);
                        command.Parameters.AddWithValue("@Instructions", string.Join(Environment.NewLine, pose.Instructions));
                        command.Parameters.AddWithValue("@Effects", string.Join(Environment.NewLine, pose.Effects));
                        command.Parameters.AddWithValue("@Tips", string.Join(Environment.NewLine, pose.Tips));
                        command.Parameters.AddWithValue("@Indications", string.Join(Environment.NewLine, pose.Indications));
                        command.Parameters.AddWithValue("@Contraindications", string.Join(Environment.NewLine, pose.Contraindications));
                        command.Parameters.AddWithValue("@AbilityLevelId", pose.AbilityLevelId);
                        command.Parameters.AddWithValue("@ImageGuid", pose.ImageGuid);
                        command.Parameters.AddWithValue("@VideoName", DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", pose.CategoryId);

                        command.ExecuteNonQuery();
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