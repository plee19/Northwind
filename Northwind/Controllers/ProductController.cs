using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Discount()
        {
            return View();
        }

        public ActionResult Unit2Project()
        {
            //ViewBag.ProductList = Products;

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
            ProductContext productContext = new ProductContext();
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
        }
    }
}