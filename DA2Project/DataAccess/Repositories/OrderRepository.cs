using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;
using Dtos.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContextDb _dbContext;

        public OrderRepository(IContextDb orderContext)
        {
            _dbContext = orderContext;
        }

        public Order AddOrder(Order entity)
        {
            try
            {
                if (IsUnique(entity))
                {
                    var orderDto = OrderHelper.FromOrderToOrderDto(entity);

                    var cartProductIds = orderDto.Products.Select(cp => cp.Product.Id).ToList();

                    orderDto.Customer = _dbContext.Users.Find(entity.Customer.Id);

                    var productsDb = _dbContext.Products
                        .Where(cp => cartProductIds.Contains(cp.Id))
                        .ToList();

                    foreach (var cartProductDto in orderDto.Products)
                    {
                        var productFromDb = productsDb.FirstOrDefault(p => p.Id == cartProductDto.Product.Id);
                        if (productFromDb != null)
                        {
                            cartProductDto.Product = productFromDb;
                        }
                    }

                    _dbContext.Orders.Add(orderDto);
                    _dbContext.SaveChanges();

                    return entity;
                }
                else
                {
                    throw new InstanceNotUniqueException("Order already exists");
                }
            }
            catch (InstanceNotUniqueException inuEx)
            {
                throw inuEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                var orders = _dbContext.Orders
                 .Include(o => o.Customer)
                    .ThenInclude(c => c.Address)
                 .Include(o => o.Products)
                     .ThenInclude(p => p.Product)
                         .ThenInclude(p => p.BrandDto)
                 .Include(o => o.Products)
                     .ThenInclude(p => p.Product)
                         .ThenInclude(p => p.ColorDto)
                 .Include(o => o.Products)
                     .ThenInclude(p => p.Product)
                         .ThenInclude(p => p.CategoryDto)
                 .Include(o => o.AppliedPromotion)
                 .Include(o => o.PaymentMethod)
                 .ToList();

                if (orders.Count == 0)
                    throw new NoInstanceException("No orders found");

                return orders.Select(o => OrderHelper.FromOrderDtoToOrder(o)).ToList();
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

        private bool IsUnique(Order entity)
        {
            return !_dbContext.Orders.Any(x => x.Id == entity.Id);
        }
    }
}
