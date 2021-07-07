using System;
using System.Configuration;

namespace Common
{
    public class ConfigurationFunctions
    {
        public static T GetAppSetting<T>(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static T GetSection<T>(string name) where T : ConfigurationSection
        {
            return ConfigurationManager.GetSection(name) as T;
        }
    }
}
