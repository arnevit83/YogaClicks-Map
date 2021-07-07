using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Clicks.Yoga.Portal.Validation;
using Common.ExceptionHandling;
using FluentValidation.Mvc;

namespace Clicks.Yoga.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new ValidatorFactory()));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            if (ex == null)
                return;

            var exHttp = ex as HttpException;

            if (exHttp != null)
            {
                if (exHttp.GetHttpCode() != 500)
                    return;

                if (exHttp.InnerException != null)
                    ex = exHttp.InnerException;
            }

            ExceptionHandler.Handle(ex);
        }
    }
}