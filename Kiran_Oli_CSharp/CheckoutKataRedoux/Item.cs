namespace CheckoutKataRedoux
{
    using System.ComponentModel.Design;

    public class Item
    {
        public Item(double price = 0.0)
        {
            Price = price;
        }

        public double Price { get; private set; }

        public string Name { get; set; }

        public int DiscountMultiple { get; set; }

        public double Discount { get; set; }

        public int Quantity { get; set; }

        public double MultiItemDiscount { get; set; }

        public string MultiDiscountItems { get; set; }

        protected bool Equals(Item other)
        {
            return string.Equals(this.Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Item)obj);
        }

        public override int GetHashCode()
        {
            return (this.Name != null ? this.Name.GetHashCode() : 0);
        }

        public double GetFinalPrice()
        {
            double subtotal = this.Price * this.Quantity;

            //while (this.Quantity >= this.DiscountMultiple)
            //{
            //    subtotal -= this.Discount;
            //    this.Quantity -= this.DiscountMultiple;
            //}

            if (this.DiscountMultiple > 0)
            {
                subtotal -= (this.Discount * (this.Quantity / this.DiscountMultiple)); 
            }

            return subtotal;
        }
    }
}