using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Clicks.Yoga.Context;

namespace Clicks.Yoga.Portal
{
    public class YogaApiController : ApiController
    {
        public YogaApiController(IWorkUnit workUnit, ISecurityContext securityContext)
        {
            WorkUnit = workUnit;
            SecurityContext = securityContext;
        }

        protected IWorkUnit WorkUnit { get; set; }

        internal ISecurityContext SecurityContext { get; set; }
    }
}