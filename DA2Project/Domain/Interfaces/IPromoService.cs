using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPromoService
    {
        Promotion GetPromo(Guid Id);
        IEnumerable<Promotion> GetAllPromos();
        Promotion ApplyPromo(List<CartProduct> cartProducts);
    }
}
