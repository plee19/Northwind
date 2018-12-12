using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                return View(db.Categories.OrderBy(c => c.CategoryName).ToList());
            }
        }

        public JsonResult FilterChart(int? categoryID)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                var categoryInfo =
                    from p in db.Products
                    join c in db.Categories on p.CategoryID equals c.CategoryID
                    join od in db.Order_Details on p.ProductID equals od.ProductID
                    group new { p, c, od } by new { c.CategoryName } into f
                    select new { CategoryName = f.Key.CategoryName, Sum = (decimal?)f.Sum(s => s.od.Quantity * s.od.UnitPrice * (1 - s.od.Discount)) };  //Sum = (od.UnitPrice * od.Quantity * (1 - od.Discount)) };

                var catInfo = categoryInfo.ToList();

                return Json(catInfo, JsonRequestBehavior.AllowGet);
            } 
        }

        // GET: Chart/1
        /*public JsonResult Chart(int id)
        {

        }*/
    }
}