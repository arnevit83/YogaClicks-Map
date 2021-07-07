using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clicks.Yoga.Portal.Controllers
{
    public class UrlReWriteController : Controller
    {
        //
        // GET: /UrlReWrite/

        public ActionResult Index(string slug)
        {

            ViewBag.Name = slug;

            return View();
        }

    }
}
