using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class FooController : Controller
    {
        // GET: Foo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bar()
        {
            ViewBag.chewingGum = "Big Red";
            return View();
        }
    }
}