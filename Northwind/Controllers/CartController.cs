using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public JsonResult AddToCart(CartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

            // create cart item from Json object
            Cart sc = new Cart();
            sc.ProductID = cartDTO.ProductID;
            sc.CustomerID = cartDTO.CustomerID;
            sc.Quantity = cartDTO.Quantity;

            // save changes
            using (NorthwindEntities db = new NorthwindEntities())
            {
                /*if there is a duplicate product id in cart, simply update the quantity
                if (db.Carts.SingleOrDefault(c => c.ProductID == sc.ProductID &&
                c.CustomerID == sc.CustomerID))
                {   
                    if (cart != null)
                    {
                        cart.Quantity += cartDTO.Quantity;
                    }
                }
                else
                {
                    // cart does not exist
                    sc.Quantity = cartDTO.Quantity;
                    db.Carts.Add(sc);
                }*/
            }
            return Json(sc, JsonRequestBehavior.AllowGet);
        }
    }
}