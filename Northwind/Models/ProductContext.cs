using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Models
{
    public class ProductContext
    {
        public List<Product> GetAll()
        {
            // return _products;
            return new List<Northwind.Models.Product>()
            {
                new Product { ProductId ="100", Name="Genen Shouyu", Price=13.99 },
                new Product { ProductId ="101", Name="Schoggi Schokolade", Price=4.49 },
                new Product { ProductId ="102", Name="Chef Anton's Italian Seasoning", Price=5.99},
                new Product { ProductId ="103", Name="Palermo's Primo Thin Pizza", Price=7.99},
                new Product { ProductId ="104", Name="Coca-Cola Zero Sugar 20oz", Price=1.99}
            };
        }
    }
}