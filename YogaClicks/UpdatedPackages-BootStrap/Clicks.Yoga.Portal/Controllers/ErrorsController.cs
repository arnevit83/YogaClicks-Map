using System;
using System.Net;
using System.Web.Mvc;

namespace Clicks.Yoga.Portal.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Error()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Error");
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View("Forbidden");
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("NotFound");
        }

        public ActionResult Deleted()
        {
            Response.StatusCode = (int)HttpStatusCode.Gone;
            return View("Deleted");
        }

        public ActionResult Test()
        {
            throw new Exception();
        }
    }
}