using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stuff(int id = 0)
        {
            ViewBag.qty = id;
            return View();
        }

        public ActionResult AddNumbers(FormCollection form)
        {
            double num1;
            double num2;

            var num1Success = Double.TryParse(form["num1"], out num1);
            var num2Success = Double.TryParse(form["num2"], out num2);

            if (num1Success && num2Success)
            {
                ViewBag.Total = num1 + num2;
                ViewBag.number1 = num1;
                ViewBag.number2 = num2;
            }



            // ViewBag.Total = ViewBag.number1 + ViewBag.number2 + ViewBag.number3;
            return View();
        }
        public ActionResult DisplayList()
        {
            ViewBag.Names = new string[] { "Joe", "Jim", "Janice", "Joan" };
            return View();
        }

        public ActionResult PrimaryColors()
        {
            return View();
        }

        public ActionResult Lesson2()
        {
            return View();
        }

        public ActionResult Lesson3()
        {
            return View();
        }

        public ActionResult Lesson4()
        {
            return View();
        }

        public ActionResult Lesson5()
        {
            return View();
        }
    }
}