using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Bake()
        {
            ViewBag.Pizza = "meat lover's pizza";
            return View();
        }

        public ActionResult Eat(int? id)
        {
            List<int?> list = null;

            if (id != null)
            {
                list = new List<int?>();
            }
            list?.Add(id);
            ViewBag.PiecesAte = id;
            return View();
        }
    }
}