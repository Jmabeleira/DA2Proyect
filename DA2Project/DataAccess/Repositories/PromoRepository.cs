using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;

namespace DataAccess.Repositories
{
    public class PromoRepository : IPromoRepository
    {

        private readonly IReflectionManager _reflectionManager;

        public PromoRepository(IReflectionManager reflectionManager)
        {
            _reflectionManager = reflectionManager;
        }

        public IEnumerable<Promotion> GetAllPromos()
        {
            try
            {
                var promos = _reflectionManager.GetAllPromotions();
                if (promos.Count != 0)
                {
                    return promos;

                }
                throw new NoInstanceException("There are no promos");
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public Promotion GetPromo(Guid Id)
        {
            try
            {
                var promo = _reflectionManager.GetAllPromotions().FirstOrDefault(p => p.Id == Id);
                if (promo != null)
                {
                    return promo;
                }
                throw new NoInstanceException("Promo dosent Exist");
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }
    }
}
