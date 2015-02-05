using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dojo.Feb15.CheckoutKata
{
    [TestFixture]
    public class CheckoutTests
    {
        private Checkout _co;

        // Sample test items
        private static Item apple = new Item("A", "Apple", 0.3m);

        private static Item beans = new Item("B", "Beans", 0.5m);
        private static Item coke = new Item("C", "Coke", 1.8m);
        private static Item deodorant = new Item("D", "Deodorant", 2.5m);
        private static Item egg = new Item("E", "Egg", 1.2m);

        private const string A = "A";
        private const string B = "B";
        private const string C = "C";
        private const string D = "D";
        private const string E = "E";

        // Sample rules
        private static IDiscountRule appleDiscount = new MultipleItemDiscount(apple, 4, 0.2m);

        private static IDiscountRule deodorantDiscount = new MultipleItemDiscount(deodorant, 2, 0.5m);
        private static IDiscountRule beansAndCokeGroupDiscount = new GroupItemDiscount(0.3m, coke, beans);

        private static IEnumerable<IDiscountRule> discountRuleSet = new[]
            {
                appleDiscount,
                deodorantDiscount,
                beansAndCokeGroupDiscount
            };

        private static IEnumerable<Item> GetItemsHelper(params string[] itemSkus)
        {
            Item[] availableItems = new[] { apple, beans, coke, deodorant, egg };

            foreach (var sku in itemSkus)
            {
                yield return availableItems.FirstOrDefault(x => x.SKU == sku);
            }
        }

        [SetUp]
        public void Setup()
        {
            _co = new Checkout();
            _co.DiscountRules = discountRuleSet;
        }

        [Test]
        public void CalculateTotal_WithNoItems_ReturnsZero()
        {
            var expected = 0;

            Decimal actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithOneItem_ReturnsPriceOfItem()
        {
            var expected = apple.Price;

            _co.Scan(apple);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithTwoApples_ReturnsPriceOfTwoApples()
        {
            var expected = apple.Price * 2;

            _co.Scan(apple, apple);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithFourApples_ReturnsDiscountedPrice()
        {
            var expected = 1m;

            _co.Scan(apple, apple, apple, apple);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithFiveApples_OnlyAppliesDiscountToFour()
        {
            var expected = 1m + apple.Price;

            _co.Scan(apple, apple, apple, apple, apple);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithFourApplesAndTwoDeodorant_AppliesBothDiscounts()
        {
            var expected = (apple.Price * 4) - 0.2m + (deodorant.Price * 2) - 0.5m;

            _co.Scan(apple, apple, apple, apple, deodorant, deodorant);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithFiveApplesAndFiveDeodorant_AppliesBothDiscounts()
        {
            var expected = (apple.Price * 5) - 0.2m + (deodorant.Price * 5) - 1.0m;

            _co.Scan(apple, apple, apple, apple, apple, deodorant, deodorant, deodorant, deodorant, deodorant);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithBeansAndCoke_AppliesGroupDiscount()
        {
            var expected = beans.Price + coke.Price - 0.3m;

            _co.Scan(beans, coke);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CalculateTotal_WithTwoBeansAndOneCoke_AppliesGroupDiscount()
        {
            var expected = (beans.Price * 2) + coke.Price - 0.3m;

            _co.Scan(beans, coke, beans);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(0.3, A)]
        [TestCase(0.3 * 2, A, A)]
        [TestCase(0.3 * 3, A, A, A)]
        [TestCase(0.3 * 4 - 0.2, A, A, A, A)]
        [TestCase(0.3 * 5 - 0.2, A, A, A, A, A)]
        [TestCase(0.3 * 6 - 0.2, A, A, A, A, A, A)]
        [TestCase(0.3 * 7 - 0.2, A, A, A, A, A, A, A)]
        [TestCase(0.3 * 8 - 0.4, A, A, A, A, A, A, A, A)]
        [TestCase(0.3 * 9 - 0.4, A, A, A, A, A, A, A, A, A)]
        [Test]
        public void CalculateTotal_WithMultipleApples_CalculatesExpectedResult(decimal expected, params string[] itemSkus)
        {
            var items = GetItemsHelper(itemSkus).ToArray();

            _co.Scan(items);

            var actual = _co.CalculateTotal();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}