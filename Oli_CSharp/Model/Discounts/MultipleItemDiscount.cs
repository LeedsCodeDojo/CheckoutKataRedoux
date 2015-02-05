using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Feb15.CheckoutKata
{
    /// <summary>
    /// Discount rule for multiple, indentical items (ie. 3 for 2)
    /// </summary>
    public class MultipleItemDiscount : IDiscountRule
    {
        public MultipleItemDiscount(CheckoutKata.Item item, int multiple, decimal discount)
        {
            this.Item = item;
            this.Multiple = multiple;
            this.Discount = discount;
        }

        /// <summary>
        /// The item for which the discount applies.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// The multiple of items for which the discount applied.
        /// </summary>
        public int Multiple { get; private set; }

        public decimal Discount { get; private set; }

        IEnumerable<Item> IDiscountRule.GetDiscountedItems(IEnumerable<Item> allItems)
        {
            return allItems.Where(x => x.SKU == Item.SKU)
                        .Where(x => allItems.Count(e => e.SKU == Item.SKU) >= Multiple)
                        .Take(Multiple);
        }
    }
}