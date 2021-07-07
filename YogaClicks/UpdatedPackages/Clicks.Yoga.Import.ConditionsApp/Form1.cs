using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clicks.Yoga.Import.ConditionsApp
{
    public partial class Form1 : Form
    {
        public Form1(
            IWorkUnit workUnit,
            IMedicService medicService)
        {
            InitializeComponent();
            MedicService = medicService;
            WorkUnit = workUnit;
        }

        public IMedicService MedicService { get; set; }

        public IWorkUnit WorkUnit { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            var conditions = new List<Condition>();

            var filePath = @"C:\ym";
            var fileName = Path.Combine(filePath, "test.xlsx");

            using (var con = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", fileName)))
            {
                con.Open();

                using (var cmd = new OleDbCommand("select * from [Sheet1$]", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Condition condition = null;

                        while (reader.Read())
                        {
                            condition = new Condition
                            {
                                Name = EnsureString(reader["Condition"]),
                                ImageCourtesyOf = EnsureString(reader["Image credit"]),
                                Description = EnsureString(reader["About"])
                            };


                            string[] yoga = EnsureString(reader["What the science says Yoga"]).Split(',');
                            string[] meditation = EnsureString(reader["What the science says Meditation Studies"]).Split(',');
                            string[] mindfulness = EnsureString(reader["What the science says Mindfulness "]).Split(',');

                            var wtss = new List<WhatTheScienceSays>();

                            foreach (var item in yoga)
                            {
                                var therapyList = new List<TherapyType>();
                                therapyList.Add(MedicService.GetTherapyTypeByName("Yoga"));

                                wtss.Add(new WhatTheScienceSays
                                {
                                    Active = true,
                                    CreationTime = DateTime.Now,
                                    Description = item,
                                    ModificationTime = DateTime.Now,
                                    TherapyTypes = therapyList
                                });
                            }

                            foreach (var item in meditation)
                            {
                                var therapyList = new List<TherapyType>();
                                therapyList.Add(MedicService.GetTherapyTypeByName("Meditation"));

                                wtss.Add(new WhatTheScienceSays
                                {
                                    Active = true,
                                    CreationTime = DateTime.Now,
                                    Description = item,
                                    ModificationTime = DateTime.Now,
                                    TherapyTypes = therapyList
                                });
                            }

                            foreach (var item in mindfulness)
                            {
                                var therapyList = new List<TherapyType>();
                                therapyList.Add(MedicService.GetTherapyTypeByName("Mindfulness"));

                                wtss.Add(new WhatTheScienceSays
                                {
                                    Active = true,
                                    CreationTime = DateTime.Now,
                                    Description = item,
                                    ModificationTime = DateTime.Now,
                                    TherapyTypes = therapyList
                                });
                            }

                            condition.WhatTheScienceSayses = wtss;

                            conditions.Add(condition);
                        }
                    }
                }
            }

            foreach (var condition in conditions)
            {
                MedicService.AddCondition(condition);
            }

            WorkUnit.Commit();
        }

        private static string EnsureString(object value)
        {
            if (value is DBNull) return "";
            return (string)value;
        }
    }
}
