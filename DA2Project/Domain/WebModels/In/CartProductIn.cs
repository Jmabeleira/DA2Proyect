using Domain.Models;

namespace Domain.WebModels.In
{
    public class CartProductIn
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartProduct ToEntity()
        {
            return new CartProduct
            {
                Id = Guid.NewGuid(),
                Product = Product,
                Quantity = Quantity
            };
        }
    }
}
