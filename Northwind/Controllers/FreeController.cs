using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class FreeController : Controller
    {
        // GET: Free
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bird(string name, int? age, string color)
        {
            ViewBag.name = name;
            ViewBag.age = age;
            ViewBag.color = color;
            return View();
        }
    }
}