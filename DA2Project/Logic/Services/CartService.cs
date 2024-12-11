using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Exceptions;
using Logic.Exceptions;

namespace Logic.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPromoService _promoService;

        public CartService(ICartRepository cartRepository, IPromoService promoService)
        {
            _cartRepository = cartRepository;
            _promoService = promoService;
        }

        public Cart AddCart(Cart cart)
        {
            try
            {
                cart.Promotion = _promoService.ApplyPromo(cart.Products);
                return _cartRepository.AddCart(cart);
            }
            catch (DANotUniqueException ex)
            {
                throw new AlreadyExistsException("Cart already exists", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Cart AddProductToCart(Guid cartId, Product product)
        {
            try
            {
                if (product.Stock == 0)
                    throw new NoStockException("Product has no Stock");

                var oldCart = _cartRepository.GetCart((Cart c) => c.Id == cartId);
                oldCart.AddProduct(product);
                oldCart.Promotion = _promoService.ApplyPromo(oldCart.Products);
                return _cartRepository.ModifyCart(oldCart);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("Cart does not exist", ex);
            }
            catch (NoStockException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public bool DeleteCartById(Guid Id)
        {
            try
            {
                return _cartRepository.DeleteCartById(Id);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("Cart does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Cart GetCartById(Guid Id)
        {
            try
            {
                return _cartRepository.GetCart((Cart c) => c.Id == Id);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("Cart does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Cart GetCartByUserId(Guid userId)
        {
            try
            {
                return _cartRepository.GetCart((Cart c) => c.User.Id == userId);
            }
            catch (DANoInstanceException)
            {
                var userCart = _cartRepository.AddCart(new Cart
                {
                    Id = Guid.NewGuid(),
                    User = new User { Id = userId },
                    Products = new List<CartProduct>() { }
                });
                return userCart;
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Cart ModifyCart(Cart cart)
        {
            try
            {
                cart.Promotion = _promoService.ApplyPromo(cart.Products);
                return _cartRepository.ModifyCart(cart);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("Cart does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }
    }
}
