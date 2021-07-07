using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;

namespace Common.ExceptionHandling.ServiceBehavior
{
    public class ExceptionHandlerBehavior : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ExceptionHandlerServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ExceptionHandlerServiceBehavior();
        }
    }
}
