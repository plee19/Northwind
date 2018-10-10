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
        string[] balloons = { "Red", "Blue", "Green", "Purple" };
        public ActionResult Birthday()
        {

            ViewBag.balloons = balloons;
            return View();
        }

        [HttpPost]
        public ActionResult Birthday(FormCollection form)
        {
            List<string> balloonList = new List<string>();
            ViewBag.ResultName = form["name"];
            ViewBag.ResultBD = form["birthday"];

            /*var balloons = form.AllKeys
                .Where(k => k.StartsWith("balloon"))
                .Select(k => form[k]);*/

            foreach (var balloon in balloons)
            {
                var b = form[balloon];
                var checker = b.Split(',');
                var isChecked = Convert.ToBoolean(checker[0]);
                if (isChecked)
                {
                    balloonList.Add(balloon);
                }
            }
            ViewBag.BalloonList = balloonList;

            return View("Results");
        }

    }
}