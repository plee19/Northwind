using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Models
{
    public class Order
    {
        public Product Prod { get; set; }
        public int Qty { get; set; }
    }
}