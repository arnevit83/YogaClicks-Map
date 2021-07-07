using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Import.TeacherTrainingImport
{
    class Organisation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Founder { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string ImageCourtesyOf { get; set; }

        public Guid ImageGuid { get; set; }
    }

    class Course
    {
        public Guid ImageGuid;

        public int OrgId { get; set; }

        public string StyleName { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public string PreReq { get; set; }

        public string Duration { get; set; }

        public string Notes { get; set; }

        public string Accreditation { get; set; }

        public string ImageCourtesyOf { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var orgs = new Dictionary<string, Organisation>();
            var courses = new List<Course>();
            var directory = ConfigurationManager.AppSettings["ExcelFileDirectory"];
            var filePath = Path.Combine(directory, ConfigurationManager.AppSettings["ExcelFileName"]);
            var imageStorePath = ConfigurationManager.AppSettings["ImageStorePath"];
            var imagesPath = Path.Combine(directory, "Image");
            var image = Path.Combine(imagesPath, "Mum and child.jpg");
            var id = 1;

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", filePath)))
            {
                con.Open();
                using (var cmd = new OleDbCommand("select * from [Sheet1$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Organisation org = null;
                        Course course = null;

                        while (reader.Read())
                        {
                            if (EnsureString(reader["Organisation Name"]) != "")
                            {
                                var orgName = EnsureString(reader["Organisation Name"]);

                                if (!orgs.ContainsKey(orgName))
                                {
                                    org = new Organisation();
                                    org.Id = id;
                                    org.Name = orgName;
                                    org.Founder = EnsureString(reader["Organisation Founder"]);
                                    org.Town = EnsureString(reader["OrgTown"]);
                                    org.County = EnsureString(reader["OrgCounty"]);
                                    org.Country = EnsureString(reader["OrgCountry"]);

                                    orgs.Add(orgName, org);

                                    id++;
                                }
                                else
                                {
                                    org = orgs[orgName];
                                }

                                course = new Course();

                                course.OrgId = org.Id;
                                course.Name = EnsureString(reader["Name of Course"]);
                                course.Website = EnsureString(reader["Course Website"]);
                                course.PreReq = EnsureString(reader["Prerequisites"]);
                                course.Duration = EnsureString(reader["Course duration"]);
                                course.Notes = EnsureString(reader["Course notes"]);
                                course.Accreditation = EnsureString(reader["Accreditation"]);
                                course.StyleName = EnsureString(reader["Style"]);
                                course.ImageGuid = Guid.NewGuid();
                                course.ImageCourtesyOf = "Robert Sturman";

                                courses.Add(course);
                            }
                        }
                    }
                }
            }

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaClicksImport"].ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand("insert TeacherTrainingOrganisations (Id, Name, Founder, Town, County, Country) values (@id, @name, @founder, @town, @county, @country)", con))
                {
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@founder", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@town", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@county", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@country", System.Data.SqlDbType.NVarChar, 100);

                    foreach (var org in orgs)
                    {
                        cmd.Parameters["@id"].Value = org.Value.Id;
                        cmd.Parameters["@name"].Value = org.Value.Name;
                        cmd.Parameters["@founder"].Value = org.Value.Founder;
                        cmd.Parameters["@town"].Value = org.Value.Town;
                        cmd.Parameters["@county"].Value = org.Value.County;
                        cmd.Parameters["@country"].Value = org.Value.Country;

                        cmd.ExecuteNonQuery();
                    }
                }

                using (var cmd = new SqlCommand("insert TeacherTrainingCourses (OrganisationId, Name, Website, PreReq, Duration, Notes, Accreditation, ImageGuid, ImageCourtesyOf, StyleName) values (@orgId, @name, @website, @pre, @duration, @notes, @accred, @imageGuid, @imageCourtesy, @style)", con))
                {
                    cmd.Parameters.Add("@orgId", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@website", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@pre", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@duration", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@notes", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@accred", System.Data.SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@imageGuid", System.Data.SqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("@imageCourtesy", System.Data.SqlDbType.NVarChar);
                    cmd.Parameters.Add("@style", System.Data.SqlDbType.NVarChar, 255);

                    foreach (var course in courses)
                    {
                        File.Copy(image, Path.Combine(imageStorePath, string.Format("{0}.jpg", course.ImageGuid)));

                        cmd.Parameters["@orgId"].Value = course.OrgId;
                        cmd.Parameters["@name"].Value = course.Name;
                        cmd.Parameters["@website"].Value = course.Website;
                        cmd.Parameters["@pre"].Value = course.PreReq;
                        cmd.Parameters["@duration"].Value = course.Duration;
                        cmd.Parameters["@notes"].Value = course.Notes;
                        cmd.Parameters["@accred"].Value = course.Accreditation;
                        cmd.Parameters["@imageGuid"].Value = course.ImageGuid;
                        cmd.Parameters["@imageCourtesy"].Value = course.ImageCourtesyOf;
                        cmd.Parameters["@style"].Value = course.StyleName;

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
