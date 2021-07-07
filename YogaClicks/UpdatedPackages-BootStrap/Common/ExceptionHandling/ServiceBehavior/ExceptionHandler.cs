using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;

namespace Common.ExceptionHandling.ServiceBehavior
{
    class ExceptionHandlerAccessor : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return false;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            ExceptionHandler.Handle(error);
        }
    }
}
