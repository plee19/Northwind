using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Security;
using Northwind.Models;
using System.Web.Security;

namespace Northwind.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize]
        public ActionResult Account()
        {
            ViewBag.CustomerId = UserAccount.GetUserId();
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                // encrypt the password
                customer.UserGuid = System.Guid.NewGuid();
                UserAccount.HashSHA1(customer.Password + customer.UserGuid);

                // save to database
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SignIn()
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                var companies = db.Customers.OrderBy(c => c.CompanyName).ToList();
                ViewBag.CustomerID = new SelectList(companies, "CustomerId", "CompanyName");
                return View();
            }

        }

        // Limit mapped/visible data as compared to full Customer table
        [HttpPost]
        public ActionResult SignIn(CustomerViewModel customerViewModel, string ReturnUrl)
        {
            using (NorthwindEntities db = new NorthwindEntities())
            {
                Customer c = db.Customers.Find(customerViewModel.CustomerId);
                string hashEnteredPassword = UserAccount.HashSHA1(customerViewModel.Password + c.UserGuid);
                if (hashEnteredPassword == c.Password)
                {
                    FormsAuthentication.SetAuthCookie(c.CustomerID.ToString(), false);
                    return RedirectToAction("Index", "Home");
                } else
                {
                    var companies = db.Customers.OrderBy(x => x.CompanyName).ToList();
                    ViewBag.CustomerId = new SelectList(companies, "CustomerId", "CompanyName");
                }
                return View();
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}