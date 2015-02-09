namespace CheckoutKataRedoux
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class Checkout
    {
        private readonly List<Item> items;

        public Checkout()
        {
            this.items = new List<Item>();
        }

        public double CalculateTotal()
        {
            var subtotal = this.items.Sum(x => x.GetFinalPrice());

            var multiDiscountItems = this.items.Where(x => !string.IsNullOrEmpty(x.MultiDiscountItems));

            foreach (var multiDiscountItem in multiDiscountItems)
            {
                var multiItemDiscountItem =
                    this.items.FirstOrDefault(x => x.Equals(multiDiscountItem));
                if (multiItemDiscountItem != null)
                {
                    subtotal -= (multiDiscountItem.MultiItemDiscount
                                 * Math.Min(multiDiscountItem.Quantity, multiItemDiscountItem.Quantity));
                }
            }

            return subtotal;
        }

        public void Scan(params Item[] itemsInput)
        {
            foreach (var item in itemsInput)
            {
                if (this.items.Contains(item))
                {
                    this.items.First(x => x.Equals(item)).Quantity++;
                }
                else
                {
                    item.Quantity = 1;
                    this.items.Add(item);
                }
            }
        }

        public int Count
        {
            get
            {
                return items.Sum(x => x.Quantity);
            }
        }
    }
}