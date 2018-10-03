using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Account()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Signin()
        {
            return View();
        }
    }
}