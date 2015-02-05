using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Feb15.CheckoutKata
{
    /// <summary>
    /// Discount rule for combinations of items.
    /// </summary>
    public class GroupItemDiscount : IDiscountRule
    {
        public GroupItemDiscount(decimal discount, params Item[] groupItems)
        {
            this.Discount = discount;
            this.GroupItems = groupItems;
        }

        /// <summary>
        /// A list of two or more items that must be present for the discount to be applied.
        /// </summary>
        public IEnumerable<Item> GroupItems
        {
            get;
            private set;
        }

        public decimal Discount
        {
            get;
            private set;
        }

        public IEnumerable<Item> GetDiscountedItems(IEnumerable<Item> allItems)
        {
            var validItems = allItems.Where(x => GroupItems.Any(e => e == x)).Distinct();

            if (validItems.Count() == GroupItems.Count())
            {
                return validItems;
            }

            return null;
        }
    }
}