namespace CheckoutKataRedoux.UnitTests
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class CheckoutTests
    {
        private Checkout _checkout;

        [SetUp]
        public void SetUp()
        {
            _checkout = new Checkout();
        }

        [Test]
        public void CalculateTotal_StandardCall_ResultAsExpected()
        {
            // Arrange
            var expected = 0.0;

            // Act
            var actual = _checkout.CalculateTotal();

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Scan_AcceptsItem_ResultAsExpected()
        {
            // Arrange
            var expected = 1;
            var item = new Item();
            this._checkout.Scan(item);

            // Act
            var actual = _checkout.Count;

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Scan_MultipleItems_CountIncremented()
        {
            // Arrange
            var items = new List<Item> { new Item(), new Item(), new Item(), new Item(), new Item(), };
            var expected = items.Count;
            this._checkout.Scan(items.ToArray());

            // Act
            var actual = _checkout.Count;

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void CalculateTotal_SingleItem_ReturnsCostOfSingleItem()
        {
            // Arrange
            var expected = 0.3;
            var item = new Item(price: expected);


            // Act
            _checkout.Scan(item);
            var actual = _checkout.CalculateTotal();

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Scan_WithMultipleItems_IncrementsItemQuantity()
        {
            // Arrange
            var expected = 2;
            var item = new Item() { Name = "Apples"};

            // Act
            _checkout.Scan(item, item);
        
            // Assert
            var actual = item.Quantity;
            actual.Should().Be(expected);
        }

        [Test]
        public void CalculateTotal_ItemsWithDiscount_DiscountProvidedToCost()
        {
            // Arrange
            var expected = 1.0;
            var item = new Item(0.30) { Name = "Apple", DiscountMultiple = 4, Discount = 0.2 };
            var items = new Item[] { item, item, item, item };

            // Act
            _checkout.Scan(items);
            var actual = _checkout.CalculateTotal();

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void CalculateTotal_ItemsWithDoubleDiscount_DiscountProvidedToCost()
        {
            // Arrange
            var expected = 2.0;
            var item = new Item(0.30) { Name = "Apple", DiscountMultiple = 4, Discount = 0.2 };
            var items = new Item[] { item, item, item, item, item, item, item, item };

            // Act
            _checkout.Scan(items);
            var actual = _checkout.CalculateTotal();

            // Assert
            Assert.AreEqual(expected, Math.Round(actual, 2));
        }
        
        [Test]
        public void CalculateTotal_MultipleItemsWithDoubleDiscount_DiscountProvidedToCost()
        {
            // Arrange
            var expected = 6.5;
            var apple = new Item(0.30) { Name = "Apple", DiscountMultiple = 4, Discount = 0.2 };
            var deodorant = new Item(2.5) { Name = "Deodorant", DiscountMultiple = 2, Discount = 0.5 };

            var items = new Item[] { apple, apple, deodorant, apple, apple, apple, deodorant, apple, apple, apple };

            // Act
            _checkout.Scan(items);
            var actual = _checkout.CalculateTotal();

            // Assert
            Assert.AreEqual(expected, Math.Round(actual, 2));
        }

        [Test]
        public void CalculateTotal_MultipleItemsWithMixedDiscount_DiscountProvidedToCost()
        {
            // Arrange
            var expected = 2.0;
            var beans = new Item(0.5) { Name = "Beans", MultiItemDiscount = 0.3, MultiDiscountItems = "Coke"};
            var coke = new Item(1.8) { Name = "Coke" }; 

            var items = new Item[] { coke, beans };

            // Act
            _checkout.Scan(items);
            var actual = _checkout.CalculateTotal();

            // Assert
            Assert.AreEqual(expected, Math.Round(actual, 2));
        }

        [Test]
        public void CalculateTotal_MultipleItemsWithMixedDiscountAndQuantities_DiscountProvidedToCost()
        {
            // Arrange
            var expected = 5.8;
            var beans = new Item(0.5) { Name = "Beans", MultiItemDiscount = 0.3, MultiDiscountItems = "Coke" };
            var coke = new Item(1.8) { Name = "Coke" }; 

            var items = new Item[] { coke, beans, coke, beans, coke };

            // Act
            _checkout.Scan(items);
            var actual = _checkout.CalculateTotal();

            // Assert
            Assert.AreEqual(expected, Math.Round(actual, 2));
        }
    }
}