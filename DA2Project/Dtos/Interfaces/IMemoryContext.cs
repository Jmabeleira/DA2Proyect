using Domain.Models;

namespace Dtos.Interfaces
{
    public interface IMemoryContext
    {
        public static List<Cart> Carts { get; set; }
        List<Cart> GetAllCarts();
        bool AddCart(Cart cart);
        bool RemoveCart(Cart cart);
    }
}
