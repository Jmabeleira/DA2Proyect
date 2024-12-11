using Domain.Models;

namespace Domain.WebModels.Out
{
    public class CartOut
    {
        public Guid Id { get; set; }
        public UserOut User { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
        public Promotion Promotion { get; set; }
        public double TotalPrice
        {
            get { return CalculateTotalPrice(); }
        }

        private double CalculateTotalPrice()
        {
            var promoDiscount = Promotion!=null? Promotion.CalculateDiscount(Products):0;
            return Products.Sum(p => p.Product.Price * p.Quantity) - promoDiscount;
        }

        public CartOut(Cart cart)
        {
            Id = cart.Id;
            User = new UserOut(cart.User);
            Products = cart.Products;
            Promotion = cart.Promotion;
        }

        public override bool Equals(object obj)
        {
            if (obj is CartOut cartOut)
            {
                return cartOut.Id == Id &&
                    User.Equals(cartOut.User) &&
                    Products.SequenceEqual(cartOut.Products) &&
                    Promotion == cartOut.Promotion;
            }
            return false;
        }
    }
}
