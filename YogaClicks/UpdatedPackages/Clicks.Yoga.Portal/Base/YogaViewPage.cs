using System;
using System.Web.Mvc;
using Clicks.Yoga.Portal.Context;

namespace Clicks.Yoga.Portal
{
    public abstract class YogaViewPage : WebViewPage
    {
        public IPortalSecurityContext SecurityContext
        {
            get
            {
                var controller = this.ViewContext.Controller as YogaController;
                return controller == null ? null : (IPortalSecurityContext)controller.SecurityContext;
            }
        }
    }

    public abstract class YogaViewPage<TModel> : WebViewPage<TModel>
    {
        public IPortalSecurityContext SecurityContext
        {
            get
            {
                var controller = this.ViewContext.Controller as YogaController;
                return controller == null ? null : (IPortalSecurityContext)controller.SecurityContext;
            }
        }

        protected TProperty Value<TProperty>(Func<TModel, TProperty> function)
        {
            try
            {
                return function(Model);
            }
            catch (NullReferenceException)
            {
                return default(TProperty);
            }
        }
    }
}