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

        public JsonResult FilterChart(int? id)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                if (id == null)
                {
                    var categoryInfo =
                        from p in db.Products
                        join c in db.Categories on p.CategoryID equals c.CategoryID
                        join od in db.Order_Details on p.ProductID equals od.ProductID
                        group new { p, c, od } by new { c.CategoryName } into f
                        select new {
                            CategoryName = f.Key.CategoryName,
                            TotalSales = f.Sum(s =>
                                s.od.Quantity * s.od.UnitPrice * (1 - s.od.Discount)) };  //Sum = (od.UnitPrice * od.Quantity * (1 - od.Discount)) };

                    var catInfo = categoryInfo.ToList();

                    return Json(catInfo, JsonRequestBehavior.AllowGet);
                } else
                {
                    var productInfo =
                        from p in db.Products
                        join c in db.Categories on p.CategoryID equals c.CategoryID
                        join od in db.Order_Details on p.ProductID equals od.ProductID
                        where p.CategoryID == id
                        group new { p, c, od } by new { p.ProductName } into f
                        select new
                        {
                            ProductName = f.Key.ProductName, Sum = f.Sum(s =>
                                s.od.Quantity * s.od.UnitPrice * (1 - s.od.Discount))
                        };
                    var prodInfo = productInfo.ToList();
                    return Json(prodInfo, JsonRequestBehavior.AllowGet);
                }
            } 
        }

        public JsonResult YearlySales()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                var yearlySalesInfo =
                    from o in db.Orders
                    join od in db.Order_Details on o.OrderID equals od.OrderID
                    group new { o, od } by new { o.OrderDate.Value.Year } into f
                    select new
                    {
                        Year = f.Key.Year,
                        TotalSales = f.Sum(s => s.od.UnitPrice * s.od.Quantity * (1 - s.od.Discount))
                    };
                var yearSaleInfo = yearlySalesInfo.ToList();
                return Json(yearSaleInfo, JsonRequestBehavior.AllowGet);
            }
        }
    }
}