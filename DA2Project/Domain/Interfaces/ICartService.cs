using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICartService
    {
        Cart GetCartById(Guid Id);
        Cart AddCart(Cart cart);
        Cart ModifyCart(Cart cart);
        bool DeleteCartById(Guid Id);
        Cart GetCartByUserId(Guid userId);
        Cart AddProductToCart(Guid cartId, Product product);
    }
}
