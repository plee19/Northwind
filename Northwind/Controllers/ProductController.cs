using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Discount()
        {
            return View();
        }
    }
}