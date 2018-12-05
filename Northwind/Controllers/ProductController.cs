using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {

        // GET: Product/Product/1
        public ActionResult Product(int? id)
        {
            // if there is no "category" id, return Http Bad Request
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (NorthwindEntities db = new NorthwindEntities())
            {
                // save the selected category name to the ViewBag
                ViewBag.Filter = db.Categories.Find(id).CategoryName;
                // retrieve list of products
                return View(db.Products.Where(p => p.CategoryID == id && p.Discontinued == false).OrderBy(p => p.ProductName).ToList());
            }
        }

        public ActionResult Category()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                return View(db.Categories.OrderBy(c => c.CategoryName).ToList());
            }
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Discount()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                return View(db.Discounts.ToList());
            }
        }

        // POST: Product/SearchResults
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchResults(FormCollection Form)
        {
            string SearchString = Form["SearchString"];
            ViewBag.Filter = "Product";
            using (NorthwindEntities db = new NorthwindEntities())
            {
                return View("Product", db.Products.Where(p => p.ProductName.Contains(SearchString) && p.Discontinued == false).OrderBy(p => p.ProductName).ToList());
            }
        }

        public ActionResult Unit2Project()
        {
            // ViewBag.ProductList = Products;

            ProductContext p = new ProductContext();
            return View(p.GetAll());
        }

        public ActionResult ProcessOrder(FormCollection form)
        {
            ViewBag.Name = form["name"];
            ViewBag.Address = form["address"];
            ViewBag.City = form["city"];
            ViewBag.State = form["state"];
            ViewBag.Zip = form["zip"];
            List<Northwind.Models.Order> orders = new List<Northwind.Models.Order>();

            /* loop instead of the GetAll method
            Int16 qty;
            foreach (var key in form.AllKeys)
            {
                if (Int16.TryParse(form[key], out qty) && qty > 0)
                {
                    orders.Add(new Order { Prod = p, Qty = qty });
                };
            }
            */
            // initial method to get all products
            ProductContext productContext = new ProductContext();
            List<Northwind.Models.Product> products = productContext.GetAll();

            Int16 qty;

            //ProductContext productContext = new ProductContext();
            foreach (var p in products)
            {
                //form[key]
                if (Int16.TryParse(form[p.Id], out qty) && qty > 0)
                {
                    // var p = productContext.Find(key);
                    orders.Add(new Northwind.Models.Order { Prod = p, Qty = qty });
                }
            }
            Person person = new Person
            {
                name = form["name"],
                address = form["address"],
                city = form["city"],
                state = form["state"],
                zip = form["zip"]
            };

            return View(orders);
        }

        // /Product/FilterProducts/?SearchString=en&PriceFilter=15

        public JsonResult FilterProducts(string SearchString, decimal? PriceFilter = 0)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                var products = db.Products
                    .Where(p => p.UnitPrice >= PriceFilter && p.Discontinued == false)
                    .OrderBy(pn => pn.ProductName)
                    .Select(p => new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.QuantityPerUnit,
                        p.UnitsInStock,
                        p.Supplier.SupplierID
                    });
                if (!String.IsNullOrEmpty(SearchString))
                {
                    products.Where(p => p.ProductName.Contains(SearchString));
                }
                var ProductDTO = products.ToList();
                /*var ProductDTO = db.Products.Where(p => p.UnitPrice > PriceFilter)
                                       .OrderBy(p => p.ProductName)
                                       .Select(p => new
                                       {
                                           p.ProductID,
                                           p.ProductName,
                                           p.QuantityPerUnit,
                                           p.UnitPrice,
                                           p.UnitsInStock })
                                           .ToList();*/
                    return Json(ProductDTO, JsonRequestBehavior.AllowGet);
                }                
            }
        }

        /*// GET: Product/FilterProducts
        public JsonResult FilterProducts(decimal? PriceFilter)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                var productDTO = db.Products
               .Where(p => p.UnitPrice >= PriceFilter && p.Discontinued == false)
               .OrderBy(pn => pn.ProductName)
               .Select(p => new { p.ProductID,
                   p.ProductName,
                   p.QuantityPerUnit,
                   p.UnitPrice,
                   p.UnitsInStock })
               .ToList();
                return Json(productDTO, JsonRequestBehavior.AllowGet);

                // Query Syntax
                /*var Products = db.Products.Where(p => p.Discontinued == false).ToList();
                var ProductDTOs = (from p in Products.Where(p => p.UnitPrice >= PriceFilter)
                                   orderby p.ProductName
                                   select new
                                   {
                                       p.ProductID,
                                       p.ProductName,
                                       p.QuantityPerUnit,
                                       p.UnitPrice,
                                       p.UnitsInStock
                                   }).ToList();
            }
        }*/
}