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
            return View();
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
            //ViewBag.ProductList = Products;

            ProductContext p = new ProductContext();
            return View(p.GetAll());
        }

        /*public ActionResult ProcessOrder(FormCollection form)
        {
            ViewBag.Name = form["name"];
            ViewBag.Address = form["address"];
            ViewBag.City = form["city"];
            ViewBag.State = form["state"];
            ViewBag.Zip = form["zip"];
            List<Order> orders = new List<Order>();

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
            /*ProductContext productContext = new ProductContext();
            List<Product> products = productContext.GetAll();

            Int16 qty;

            // ProductContext productContext = new ProductContext();
            foreach (var p in products)
            {
                //form[key]
                if (Int16.TryParse(form[p.Id], out qty) && qty > 0)
                {
                    // var p = productContext.Find(key);
                    orders.Add(new Order { Prod = p, Qty = qty });
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
        }*/
    }
}