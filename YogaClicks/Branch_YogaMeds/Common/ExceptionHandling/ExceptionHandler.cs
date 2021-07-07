using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExceptionHandling
{
    public static class ExceptionHandler
    {
        private static readonly List<IExceptionHandler> handlers = LoadHandlers();

        private static List<IExceptionHandler> LoadHandlers()
        {
            List<IExceptionHandler> handlers = new List<IExceptionHandler>();
            IExceptionHandler       current  = null;
            Type                    type     = null;

            foreach (var element in Configuration.ExceptionHandlingSection.Configuration.Handlers.Elements)
            {
                try
                {
                    type = Type.GetType(element.TypeName, false);

                    if (type == null)
                        continue;

                    current = Activator.CreateInstance(type) as IExceptionHandler;

                    if (current == null)
                        continue;

                    handlers.Add(current);
                }
                catch { }
            }

            return handlers;
        }

        public static void Handle(Exception ex)
        {
            foreach (var handler in handlers)
            {
                try
                {
                    handler.Handle(ex);
                }
                catch {}
            }
        }
    }
}
