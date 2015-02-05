using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Feb15.CheckoutKata
{
    public class Item
    {
        public Item(string sku, string name, decimal price)
        {
            this.SKU = sku;
            this.Name = name;
            this.Price = price;
        }

        public string SKU { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }
    }
}