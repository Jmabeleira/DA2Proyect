using Domain.Models;

namespace Domain.WebModels.In
{
    public class CartIn
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public List<CartProductIn> Products { get; set; } = new List<CartProductIn>();

        public Cart ToEntity()
        {
            return new Cart
            {
                Id = Id,
                User = User,
                Products = Products.Select(p => p.ToEntity()).ToList()
            };
        }
    }
}
