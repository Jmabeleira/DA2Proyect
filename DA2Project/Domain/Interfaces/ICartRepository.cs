using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICartRepository
    {
        Cart AddCart(Cart cart);
        bool DeleteCartById(Guid Id);
        Cart GetCart(Func<Cart, bool> func);
        Cart ModifyCart(Cart cart);
    }
}
