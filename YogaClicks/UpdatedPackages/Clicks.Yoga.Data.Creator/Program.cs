using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using Clicks.Yoga.Context;
using Ninject;

namespace Clicks.Yoga.Data.Creator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //CloseConnections();
            //Database.Delete("Clicks.Yoga.Data.YogaDataContext");
            IKernel kernel = new StandardKernel(new NinjectSettings { AllowNullInjection = true, InjectNonPublic = true }, new YogaNinjectModule());
            var context = kernel.Get<IEntityContext>();
            var init = new Initializer(context);
            var db = kernel.Get<YogaDataContext>();
            init.Seed(db);
            //context.Users.Get(0);
        }

        static void CloseConnections()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["master"].ConnectionString))
            {
                var pids = new List<short>();

                 con.Open();

                using (var cmd = new SqlCommand("select spid from sysprocesses where dbid = db_id('YogaClicks')", con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            pids.Add(reader.GetInt16(0));
                    }
                }

                if (pids.Count == 0)
                    return;

                var str = new StringBuilder();

                foreach (var id in pids)
                    str.AppendFormat("kill {0}; ", id);

                using (var cmd = new SqlCommand(str.ToString(), con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}