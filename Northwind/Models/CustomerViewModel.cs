using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Northwind.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        //[MyValidation(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }

    /*public class MyValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }*/
}