using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Feb15.CheckoutKata
{
    /// <summary>
    /// Defines a discount rule.
    /// </summary>
    public interface IDiscountRule
    {
        /// <summary>
        /// The amount to be discounted for any items matching this rule.
        /// </summary>
        decimal Discount { get; }

        /// <summary>
        /// Attempts to retrieve a collection of valid items for a single instance
        /// of a discount.
        /// </summary>
        /// <param name="allItems">A list of items to check for discount rules</param>
        /// <returns>A list of items matching the discount rule. If null or empty, then no items were found.</returns>
        IEnumerable<Item> GetDiscountedItems(IEnumerable<Item> allItems);
    }
}