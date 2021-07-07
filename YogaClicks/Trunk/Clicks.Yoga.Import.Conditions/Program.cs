using Clicks.Yoga.Context;
using Clicks.Yoga.Data;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Import.Conditions
{
    class WTSS
    {
        public string Description;
        public string ConditionName;
        public string TherapyTypeName;
    }

    class Condition
    {
        public string Name;
        public string ImageCourtesyOf;
        public string Description;
    }

    class Study
    {
        public string[] ConditionsNames;
        public string[] TherapyTypesNames;
        public string Title;
        public string[] AuthorsNames;
        public string DateofStudy;
        public string Journal;
        public string VolumeIssue;
        public string Institution;
        public string Source;
        public string Abstract;
        public string NumberOfCitations;
    }

    class Meta
    {
        public string ConditionName;
        public string MetaDescription;
        public string MetaTitle;
    }

    public class Program
    {
        public Program() { }

        static void Main(string[] args)
        {
            var conditions = new List<Condition>();
            var wtss = new List<WTSS>();
            var studies = new List<Study>();
            var metas = new List<Meta>();

            var filePath = @"C:\ym";
            var conditionFileName = Path.Combine(filePath, "conditions.xlsx");
            var studiesFileName = Path.Combine(filePath, "studies.xls");
            var metaFileName = Path.Combine(filePath, "meta.xlsx");

            #region ImportConditons

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", conditionFileName)))
            {
                con.Open();

                using (var cmd = new OleDbCommand("select * from [Sheet1$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Condition condition = null;

                        while (reader.Read())
                        {
                            Console.WriteLine("Importing condtion: " + reader["Condition"].ToString());

                            condition = new Condition
                            {
                                Name = EnsureString(reader["Condition"].ToString().Trim()),
                                ImageCourtesyOf = EnsureString(reader["Image credit"]),
                                Description = EnsureString(reader["About"])
                            };

                            string[] yoga = EnsureString(reader["What the science says Yoga"]).Split('|');
                            string[] meditation = EnsureString(reader["What the science says Meditation Studies"]).Split('|');
                            string[] mindfulness = EnsureString(reader["What the science says Mindfulness "]).Split('|');

                            foreach (var item in yoga)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    wtss.Add(new WTSS
                                    {
                                        Description = item,
                                        ConditionName = condition.Name,
                                        TherapyTypeName = "Yoga"
                                    });
                                }
                            }

                            foreach (var item in meditation)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    wtss.Add(new WTSS
                                    {
                                        Description = item,
                                        ConditionName = condition.Name,
                                        TherapyTypeName = "Meditation"
                                    });
                                }
                            }

                            foreach (var item in mindfulness)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    wtss.Add(new WTSS
                                    {
                                        Description = item,
                                        ConditionName = condition.Name,
                                        TherapyTypeName = "Mindfulness"
                                    });
                                }
                            }

                            conditions.Add(condition);

                            Console.WriteLine("Condtion: " + reader["Condition"].ToString() + " imported");
                        }

                        Console.WriteLine("All conditions imported");
                    }
                }
            }
            #endregion

            #region ImportStudies

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", studiesFileName)))
            {
                con.Open();

                using (var cmd = new OleDbCommand("select * from [Sheet4$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Study study = null;

                        while (reader.Read())
                        {
                            Console.WriteLine("Importing study: " + reader["Title"].ToString());

                            var conditionNames = EnsureString(reader["Conditions"]).ToLower().Split(',');

                            for (int i = 0; i < conditionNames.Count(); i++)
                            {
                                conditionNames[i] = conditionNames[i].Trim();
                            }

                            study = new Study
                            {
                                ConditionsNames = conditionNames,
                                TherapyTypesNames = EnsureString(reader["therapy type"]).Split(','),
                                Title = EnsureString(reader["Title"]),
                                AuthorsNames = EnsureString(reader["Authors"]).Split(','),
                                DateofStudy = EnsureString(reader["Date"]),
                                Journal = EnsureString(reader["Journal"]),
                                VolumeIssue = EnsureString(reader["Volume Issue"]),
                                Institution = EnsureString(reader["Institution"]),
                                Source = EnsureString(reader["Link"]),
                                Abstract = EnsureString(reader["Abstract"]),
                                NumberOfCitations = EnsureString(reader["Number of Citations"])
                            };

                            studies.Add(study);

                            Console.WriteLine("Study: " + reader["Title"].ToString() + " imported");
                        }

                        Console.WriteLine("All studies imported");
                    }
                }
            }
            #endregion

            #region ImportMeta

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", metaFileName)))
            {
                con.Open();

                using (var cmd = new OleDbCommand("select * from [Sheet1$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Meta meta = null;

                        while (reader.Read())
                        {
                            Console.WriteLine("Importing meta for: " + reader["condition"].ToString());

                            meta = new Meta
                            {
                                ConditionName = EnsureString(reader["condition"]),
                                MetaTitle = EnsureString(reader["title"]),
                                MetaDescription = EnsureString(reader["description"])
                            };

                            metas.Add(meta);

                            Console.WriteLine("Meta for : " + reader["condition"].ToString() + " imported");
                        }

                        Console.WriteLine("All metas imported");
                    }
                }
            }
            #endregion

            foreach (var condition in conditions.OrderBy(x => x.Name))
            {
                Console.WriteLine("Inserting " + condition.Name);

                var conditionId = 0;
                var wtssIds = new List<int>();

                using (var con = new SqlConnection("server=(local)\\sqlexpress;database=yogaclicks;integrated security=true;"))
                {
                    con.Open();

                    using (var cmd = new SqlCommand("INSERT INTO Condition (Name, [Description], ImageCourtesyOf, Active, CreationTime, ModificationTime) " +
                        "values (@name, @description, @imageCourtesyOf, @active, @creationTime, @modificationTime) SELECT SCOPE_IDENTITY()", con))
                    {
                        cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                        cmd.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                        cmd.Parameters.Add("@imageCourtesyOf", System.Data.SqlDbType.NVarChar);
                        cmd.Parameters.Add("@active", System.Data.SqlDbType.Bit);
                        cmd.Parameters.Add("@creationTime", System.Data.SqlDbType.DateTime);
                        cmd.Parameters.Add("@modificationTime", System.Data.SqlDbType.DateTime);


                        cmd.Parameters["@name"].Value = condition.Name;
                        cmd.Parameters["@description"].Value = condition.Description;
                        cmd.Parameters["@imageCourtesyOf"].Value = condition.ImageCourtesyOf;
                        cmd.Parameters["@active"].Value = true;
                        cmd.Parameters["@creationTime"].Value = DateTime.Now;
                        cmd.Parameters["@modificationTime"].Value = DateTime.Now;

                        conditionId = int.Parse(cmd.ExecuteScalar().ToString());
                    }

                    con.Close();

                    foreach (var meta in metas.Where(x => x.ConditionName.Trim().ToLower() == condition.Name.Trim().ToLower()))
                    {
                        con.Open();

                        using (var cmd = new SqlCommand("UPDATE Condition SET MetaTitle = @metaTitle, MetaDescription = @metaDescription WHERE Id = @conditionId SELECT SCOPE_IDENTITY()", con))
                        {
                            cmd.Parameters.Add("@metaTitle", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@MetaDescription", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@conditionId", System.Data.SqlDbType.Int);

                            cmd.Parameters["@metaTitle"].Value = meta.MetaTitle;
                            cmd.Parameters["@MetaDescription"].Value = meta.MetaDescription;
                            cmd.Parameters["@conditionId"].Value = conditionId;

                            cmd.ExecuteNonQuery();
                        }

                        con.Close();
                    }

                    foreach (var item in wtss.Where(x => x.ConditionName == condition.Name))
                    {
                        Console.WriteLine("Inserting WTSS: " + item.TherapyTypeName + " | for " + item.ConditionName);

                        var wtssId = 0;
                        var therapyTypeId = GetTherapyTypeId(item.TherapyTypeName);
                        con.Open();

                        using (var cmd = new SqlCommand("INSERT INTO WhatTheScienceSays ([Description], Active, CreationTime, ModificationTime) " +
                            "values (@description, @active, @creationTime, @modificationTime) SELECT SCOPE_IDENTITY()", con))
                        {
                            cmd.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@active", System.Data.SqlDbType.Bit);
                            cmd.Parameters.Add("@creationTime", System.Data.SqlDbType.DateTime);
                            cmd.Parameters.Add("@modificationTime", System.Data.SqlDbType.DateTime);


                            cmd.Parameters["@description"].Value = item.Description;
                            cmd.Parameters["@active"].Value = true;
                            cmd.Parameters["@creationTime"].Value = DateTime.Now;
                            cmd.Parameters["@modificationTime"].Value = DateTime.Now;

                            wtssId = int.Parse(cmd.ExecuteScalar().ToString());

                            wtssIds.Add(wtssId);
                        }
                        con.Close();

                        Console.WriteLine("Inserting WTSS: " + item.TherapyTypeName + " | therapy type: " + item.TherapyTypeName + " link");

                        con.Open();

                        using (var cmd = new SqlCommand("INSERT INTO WhatTheScienceSaysTherapyType (TherapyType_Id, WhatTheScienceSays_Id) " +
                            "values (@therapyTypeId, @wtssId)", con))
                        {
                            cmd.Parameters.Add("@therapyTypeId", System.Data.SqlDbType.Int);
                            cmd.Parameters.Add("@wtssId", System.Data.SqlDbType.Int);

                            cmd.Parameters["@therapyTypeId"].Value = therapyTypeId;
                            cmd.Parameters["@wtssId"].Value = wtssId;

                            cmd.ExecuteNonQuery();
                        }

                        con.Close();


                    }

                    var cName = condition.Name.Trim().ToLower();

                    foreach (var study in studies.Where(x => x.ConditionsNames.Contains(cName)))
                    {
                        Console.WriteLine("Inserting study: " + study.Title);

                        var studyId = 0;

                        con.Open();

                        using (var cmd = new SqlCommand("INSERT INTO Study (Abstract, Active, CreationTime, DateOfStudy, Institution, Journal, ModificationTime, NumberOfCitations, [Source], Title, Volume) " +
                            "values (@abstract, @active, @creationTime, @dateOfStudy, @institution, @journal, @modificationTime, @numberOfCitations, @source, @title, @volume) SELECT SCOPE_IDENTITY()", con))
                        {
                            cmd.Parameters.Add("@abstract", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@active", System.Data.SqlDbType.Bit);
                            cmd.Parameters.Add("@creationTime", System.Data.SqlDbType.DateTime);
                            cmd.Parameters.Add("@dateOfStudy", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@institution", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@journal", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@modificationTime", System.Data.SqlDbType.DateTime);
                            cmd.Parameters.Add("@numberOfCitations", System.Data.SqlDbType.Int);
                            cmd.Parameters.Add("@source", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                            cmd.Parameters.Add("@volume", System.Data.SqlDbType.NVarChar);

                            cmd.Parameters["@abstract"].Value = study.Abstract;
                            cmd.Parameters["@active"].Value = true;
                            cmd.Parameters["@creationTime"].Value = DateTime.Now;
                            cmd.Parameters["@dateOfStudy"].Value = study.DateofStudy;
                            cmd.Parameters["@institution"].Value = study.Institution;
                            cmd.Parameters["@journal"].Value = study.Journal;
                            cmd.Parameters["@modificationTime"].Value = DateTime.Now;
                            cmd.Parameters["@numberOfCitations"].Value = int.Parse(study.NumberOfCitations);
                            cmd.Parameters["@source"].Value = study.Source;
                            cmd.Parameters["@title"].Value = study.Title;
                            cmd.Parameters["@volume"].Value = study.VolumeIssue;

                            studyId = int.Parse(cmd.ExecuteScalar().ToString());
                        }

                        con.Close();

                        Console.WriteLine("Study: " + study.Title + " inserted");

                        foreach (var type in study.TherapyTypesNames)
                        {
                            Console.WriteLine("Inserting study: " + study.Title + " | thereapy type link");

                            con.Open();

                            using (var cmd = new SqlCommand("INSERT INTO TherapyTypeStudy (Study_Id, TherapyType_Id) " +
                                "values (@studyId, @therapyId)", con))
                            {
                                cmd.Parameters.Add("@studyId", System.Data.SqlDbType.Int);
                                cmd.Parameters.Add("@therapyId", System.Data.SqlDbType.Int);

                                cmd.Parameters["@studyId"].Value = studyId;
                                cmd.Parameters["@therapyId"].Value = GetTherapyTypeId(type);

                                cmd.ExecuteNonQuery();
                            }

                            con.Close();

                            Console.WriteLine("Study: " + study.Title + " | thereapy type link inserted");
                        }

                        foreach (var author in study.AuthorsNames)
                        {
                            Console.WriteLine("Inserting author: " + author);

                            var authorId = 0;

                            con.Open();

                            using (var cmd = new SqlCommand("INSERT INTO Author (Active, CreationTime, ModificationTime, Name) " +
                                "values (@active, @creationTime, @modificationTime, @name) SELECT SCOPE_IDENTITY()", con))
                            {
                                cmd.Parameters.Add("@active", System.Data.SqlDbType.Bit);
                                cmd.Parameters.Add("@creationTime", System.Data.SqlDbType.DateTime);
                                cmd.Parameters.Add("@modificationTime", System.Data.SqlDbType.DateTime);
                                cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);

                                cmd.Parameters["@active"].Value = true;
                                cmd.Parameters["@creationTime"].Value = DateTime.Now;
                                cmd.Parameters["@modificationTime"].Value = DateTime.Now;
                                cmd.Parameters["@name"].Value = author;

                                authorId = int.Parse(cmd.ExecuteScalar().ToString());
                            }

                            con.Close();

                            Console.WriteLine(authorId + " inserted");

                            Console.WriteLine("Inserting author: " + author + "study: " + study.Title + " link");

                            con.Open();

                            using (var cmd = new SqlCommand("INSERT INTO AuthorStudy (Author_Id, Study_Id) " +
                                "values (@authorId, @studyId)", con))
                            {
                                cmd.Parameters.Add("@authorId", System.Data.SqlDbType.Int);
                                cmd.Parameters.Add("@studyId", System.Data.SqlDbType.Int);

                                cmd.Parameters["@authorId"].Value = authorId;
                                cmd.Parameters["@studyId"].Value = studyId;

                                cmd.ExecuteNonQuery();
                            }

                            con.Close();

                            Console.WriteLine("Author: " + author + "study: " + study.Title + " link inserted");
                        }

                        Console.WriteLine("Inserting condtion: " + condition.Name + "study: " + study.Title + " link");

                        con.Open();

                        using (var cmd = new SqlCommand("INSERT INTO StudyCondition (Condition_Id, Study_Id) " +
                            "values (@conditionId, @studyId)", con))
                        {
                            cmd.Parameters.Add("@conditionId", System.Data.SqlDbType.Int);
                            cmd.Parameters.Add("@studyId", System.Data.SqlDbType.Int);

                            cmd.Parameters["@conditionId"].Value = conditionId;
                            cmd.Parameters["@studyId"].Value = studyId;

                            cmd.ExecuteNonQuery();
                        }

                        con.Close();

                        Console.WriteLine("Condition: " + condition.Name + "study: " + study.Title + " link inserted");

                    }

                    foreach (var item in wtssIds)
                    {
                        Console.WriteLine("Inserting WTSS + " + condition.Name + " link");

                        con.Open();

                        using (var cmd = new SqlCommand("INSERT INTO WhatTheScienceSaysCondition(Condition_Id, WhatTheScienceSays_Id) " +
                            "values (@conditionId, @wtssId)", con))
                        {
                            cmd.Parameters.Add("@conditionId", System.Data.SqlDbType.Int);
                            cmd.Parameters.Add("@wtssId", System.Data.SqlDbType.Int);

                            cmd.Parameters["@conditionId"].Value = conditionId;
                            cmd.Parameters["@wtssId"].Value = item;

                            cmd.ExecuteNonQuery();
                        }

                        con.Close();
                    }
                }

                Console.WriteLine(condition.Name + " insert complete");
            }

            Console.WriteLine("All inserts complete");
        }

        private static int GetTherapyTypeId(string typeName)
        {
            typeName = typeName.TrimStart(' ');
            typeName = typeName.TrimEnd(' ');
            if (typeName == "Yoga") return 1;
            if (typeName == "Meditation") return 2;
            if (typeName == "Mindfulness") return 3;

            return 0;
        }

        private static string EnsureString(object value)
        {
            if (value is DBNull) return "";
            return (string)value;
        }
    }
}
