using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;

namespace DataAccess.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IMemoryContext _memoryContext;

        public CartRepository(IMemoryContext memoryContext)
        {
            _memoryContext = memoryContext;
        }
        public Cart AddCart(Cart cart)
        {
            try
            {
                if (!ExistsCart(cart))
                {
                    _memoryContext.AddCart(cart);
                    return cart;
                }
                else
                {
                    throw new InstanceNotUniqueException("This User already have a Cart");
                }
            }
            catch (InstanceNotUniqueException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        private bool ExistsCart(Cart cart)
        {
            var cartsDb = _memoryContext.GetAllCarts();

            if (cartsDb != null)
            {
                var isNotUnique = cartsDb.Any(c => c.User.Email == cart.User.Email);
                return isNotUnique;
            }

            return false;
        }

        public bool DeleteCartById(Guid Id)
        {
            try
            {
                var cart = GetCart((Cart c) => c.Id == Id);
                if (ExistsCart(cart))
                {
                    return _memoryContext.RemoveCart(cart);
                }

                throw new NoInstanceException("Cart Not Found");
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

        public Cart ModifyCart(Cart cart)
        {
            try
            {
                var allCarts = _memoryContext.GetAllCarts();
                var myCart = allCarts.FirstOrDefault(c => c.Id == cart.Id);

                if (myCart != null)
                {
                    myCart.Products = cart.Products;
                    myCart.Promotion = cart.Promotion;
                    myCart.User = cart.User;

                    return myCart;
                }
                throw new NoInstanceException("Cart not found");
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

        public Cart GetCart(Func<Cart, bool> func)
        {
            try
            {
                var cart = _memoryContext.GetAllCarts().FirstOrDefault(func);
                if (cart != null)
                {
                    return cart;
                }
                throw new NoInstanceException("Cart not found");
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
