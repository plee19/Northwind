using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Security;
using Northwind.Models;
using System.Web.Security;
using System.Net;

namespace Northwind.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        [Authorize]
        public ActionResult Account()
        {
            if (Request.Cookies["role"].Value != "customer")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var custId = UserAccount.GetUserId();
            Customer customer;

            using (var db = new NorthwindEntities())
            {
                customer = db.Customers.Find(UserAccount.GetUserId());
            }

            var customerEdit = Mapper.Map<CustomerEdit>(customer);

            /*CustomerEdit customerEdit = new CustomerEdit
            {
                CompanyName = customer.CompanyName,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Email = customer.Email
            };*/

            return View(customerEdit);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Account(CustomerEdit customerEdit)
        {
            using (var db = new NorthwindEntities())
            {
                var customer = db.Customers.Find(UserAccount.GetUserId());

                customer.ContactName = customerEdit.ContactName;
                //customer.CompanyName = customerEdit.CompanyName;
                customer.Address = customerEdit.Address;
                customer.City = customerEdit.City;
                customer.Region = customerEdit.Region;
                customer.PostalCode = customerEdit.PostalCode;
                customer.Country = customerEdit.Country;
                customer.Phone = customerEdit.Phone;
                customer.Fax = customerEdit.Fax;
                customer.Email = customerEdit.Email;

                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
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
                // verify not duplicate
                // database query option: Bool Any instead of Select
                if (db.Customers.Any(c => c.CompanyName == customer.CompanyName))
                {
                    return View();
                }

                // foreach option
                /*foreach (var item in db.Customers)
                {
                    if (item.CompanyName == customer.CompanyName)
                    {
                        return View();
                    }
                }*/



                // encrypt the password
                customer.UserGuid = System.Guid.NewGuid();
                customer.Password = UserAccount.HashSHA1(customer.Password + customer.UserGuid);

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
                if (ModelState.IsValid)
                {
                    Customer c = db.Customers.Find(customerViewModel.CustomerId);

                    string hashEnteredPassword = UserAccount.HashSHA1(customerViewModel.Password + c.UserGuid);

                    if (hashEnteredPassword == c.Password)
                    {
                        FormsAuthentication.SetAuthCookie(c.CustomerID.ToString(), false);

                        HttpCookie httpCookie = new HttpCookie("role");
                        httpCookie.Value = "customer";
                        Response.Cookies.Add(httpCookie);

                        if (ReturnUrl != null)
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("Password", "Incorrect Password");
                }
                var companies = db.Customers.OrderBy(x => x.CompanyName).ToList();
                ViewBag.CustomerId = new SelectList(companies, "CustomerId", "CompanyName");
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