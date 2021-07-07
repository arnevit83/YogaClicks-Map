using System.Configuration;

namespace Common.ExceptionHandling.Configuration
{
    public class ExceptionHandlerElement : ConfigurationElement
    {
        [ConfigurationProperty("typeName")]
        public string TypeName
        {
            get { return this["typeName"] as string; }
            set { this["typeName"] = value; }
        }
    }
}
