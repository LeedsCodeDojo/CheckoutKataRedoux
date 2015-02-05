using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Feb15.CheckoutKata
{
    public class Checkout
    {
        private List<Item> _items;

        public Checkout()
        {
            _items = new List<Item>();
        }

        /// <summary>
        /// All items currently scanned into the checkout.
        /// </summary>
        public IEnumerable<Item> Items
        {
            get { return _items.AsReadOnly(); }
        }

        /// <summary>
        /// The number of items currently scanned into the checkout.
        /// </summary>
        public int ItemCount
        {
            get { return _items.Count; }
        }

        /// <summary>
        /// Calculate the total price of all items in the checkout, with discounts applied.
        /// </summary>
        /// <returns></returns>
        public decimal CalculateTotal()
        {
            decimal subtotal = _items.Sum(x => x.Price);

            var _localItems = Items.ToList();

            if (this.DiscountRules != null)
            {
                foreach (var discount in this.DiscountRules)
                {
                    // Retrieve a collection of items from the checkout that are valid for discount.
                    IEnumerable<Item> _currentItems = discount.GetDiscountedItems(_localItems.ToList());

                    while (_currentItems != null && _currentItems.Count() > 0)
                    {
                        // Apply the discount for the current set of items.
                        subtotal -= discount.Discount;

                        // Remove the items from the liss.
                        foreach (var item in _currentItems)
                        {
                            _localItems.Remove(item);
                        }

                        // Look for more items that match the discount rule.
                        _currentItems = discount.GetDiscountedItems(_localItems.ToList());
                    }
                }
            }

            return subtotal;
        }

        /// <summary>
        /// Add an item to the checkout.
        /// </summary>
        /// <param name="items"></param>
        internal void Scan(params Item[] items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// A collection of discount rules which will be applied by CalculateTotal()
        /// </summary>
        public IEnumerable<IDiscountRule> DiscountRules { get; set; }
    }
}