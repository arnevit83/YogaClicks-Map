using System;
using System.Configuration;
using System.Collections.Generic;

namespace Common.ExceptionHandling.Configuration
{
    public class ExceptionHandlerCollection : ConfigurationElementCollection
    {
        public ExceptionHandlerCollection()
        {
            Elements = new List<ExceptionHandlerElement>();
        }

        public List<ExceptionHandlerElement> Elements { get; set; }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new ExceptionHandlerElement();
            Elements.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return Elements.Find(e => e == element);
        }
    }
}
