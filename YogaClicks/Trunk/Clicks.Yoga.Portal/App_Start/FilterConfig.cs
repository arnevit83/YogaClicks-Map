using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Clicks.Yoga.Domain;
using Common.ExceptionHandling;
using Common.ExceptionHandling.Custom.Aspnet;

namespace Clicks.Yoga.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleExceptionAttribute(new Dictionary<Type, int> {
                { typeof(PermissionDeniedException), 403 },
                { typeof(UnknownEntityException), 404 }
            }));
        }
    }

    public class HandleExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public HandleExceptionAttribute(IDictionary<Type, int> statuses)
        {
            Statuses = statuses;
        }

        private IDictionary<Type, int> Statuses { get; set; }

        public void OnException(ExceptionContext context)
        {
            var type = context.Exception.GetType();

            if (context.ExceptionHandled) return;
            if (context.IsChildAction) return;

            if (Statuses.ContainsKey(type))
            {
                context.HttpContext.Response.StatusCode = Statuses[type];
            }
            else if (context.HttpContext.Request.IsLocal)
            {
                ExceptionHandler.Handle(context.Exception);

                var body = new HtmlExceptionRenderer().Render(context.Exception);

                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.HttpContext.Response.TrySkipIisCustomErrors = true;
                context.HttpContext.Response.Write(body);
            }
            else
            {
                ExceptionHandler.Handle(context.Exception);

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}
