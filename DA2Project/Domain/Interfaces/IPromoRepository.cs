using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPromoRepository
    {
        Promotion GetPromo(Guid Id);
        IEnumerable<Promotion> GetAllPromos();
    }
}
