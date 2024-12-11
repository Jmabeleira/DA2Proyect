using Domain.Models;
using Dtos.Interfaces;

namespace DataAccess.Contexts
{
    public class MemoryContext : IMemoryContext
    {
        private static List<Cart> _carts;
        public static List<Cart> Carts { get { return _carts; } set { _carts = value; } }

        public List<Cart> GetAllCarts()
        {
            if (_carts == null)
            {
                _carts = new List<Cart>();

            }
            return _carts;
        }

        public bool AddCart(Cart cart)
        {
            _carts.Add(cart);
            return true;
        }

        public bool RemoveCart(Cart cart)
        {
            return _carts.Remove(cart);
        }
    }
}
