using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Exceptions;
using Logic.Exceptions;

namespace Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPurchaseService _purchaseService;

        public OrderService(IOrderRepository orderRepository, IPurchaseService purchaseService)
        {
            _orderRepository = orderRepository;
            _purchaseService = purchaseService;
        }

        public Order AddOrder(Order order)
        {
            try
            {
                return _orderRepository.AddOrder(order);
            }
            catch (DANotUniqueException ex)
            {
                throw new AlreadyExistsException("Order already exists", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                var orders = _orderRepository.GetAllOrders();

                return orders;
            }
            catch (DANoInstanceException ex)
            {
                throw new NoContentException("There are no Orders", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public List<Order> GetOrderByUserId(Guid id)
        {
            try
            {
                return _orderRepository.GetAllOrders().Where((Order o) => o.Customer.Id == id).ToList();
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("Order does not exist", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public List<Order> GetOrdersByCustomerId(Guid id)
        {
            try
            {
                var allOrders = _orderRepository.GetAllOrders();

                return allOrders.Where(o => o.Customer.Id == id).ToList();

            }
            catch (DANoInstanceException ex)
            {
                throw new NoContentException("User has no Orders", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Order Purchase(Order order, string paymentMethod)
        {
            try
            {
                var completeOrder = _purchaseService.Purchase(order, paymentMethod);
                _orderRepository.AddOrder(completeOrder);
                return completeOrder;
            }
            catch (NoStockException)
            {
                throw;
            }
            catch (PaymentMethodException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }
    }
}