using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test(string id)
        {
            try
            {
                double num1 = Double.Parse(id);
                ViewBag.value = num1;
            }
            catch (FormatException e)
            {
                System.Console.WriteLine(e.Message);
            }
            return View();
        }


    }
}