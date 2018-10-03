using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class ColorsController : Controller
    {
        // GET: Colors
        public ActionResult Index(String color1, String color2)
        {
            return View();
        }
    }
}