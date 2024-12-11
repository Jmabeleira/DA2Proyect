using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Exceptions;
using Logic.Exceptions;

namespace Logic.Services
{
    public class PromoService : IPromoService
    {
        IPromoRepository _promoRepository;

        public PromoService(IPromoRepository promoRepository)
        {
            _promoRepository = promoRepository;
        }
        public Promotion GetPromo(Guid Id)
        {
            try
            {
                var returnPromo = _promoRepository.GetPromo(Id);
                return returnPromo;
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("There is no such Promo", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public IEnumerable<Promotion> GetAllPromos()
        {
            try
            {
                return _promoRepository.GetAllPromos();

            }
            catch (DANoInstanceException ex)
            {
                throw new NoContentException("There are no Promos", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Promotion ApplyPromo(List<CartProduct> cartProducts)
        {
            try
            {
                var applicableProducts = cartProducts.Where(x => x.Product.IsPromotional);
                var applicablePromos = GetAllPromos().Where(x => x.IsApplicable(applicableProducts));
                var bestPromo = applicablePromos
                .OrderByDescending(promo => promo.CalculateDiscount(cartProducts))
                .FirstOrDefault();

                return bestPromo;
            }
            catch (DANoInstanceException ex)
            {
                throw new NoContentException("There are no Promos", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }
    }
}
