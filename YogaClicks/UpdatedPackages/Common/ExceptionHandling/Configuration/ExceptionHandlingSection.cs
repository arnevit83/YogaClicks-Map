using System.Configuration;
using Common;

namespace Common.ExceptionHandling.Configuration
{
    public class ExceptionHandlingSection : ConfigurationSection
    {
        private static readonly ExceptionHandlingSection configuration = ConfigurationFunctions.GetSection<ExceptionHandlingSection>("exceptionHandling");

        public static ExceptionHandlingSection Configuration
        {
            get { return configuration; }
        }

        [ConfigurationCollection(typeof(ExceptionHandlerElement), AddItemName = "add")]
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ExceptionHandlerCollection Handlers
        {
            get { return this[""] as ExceptionHandlerCollection; }
        }
    }
}
