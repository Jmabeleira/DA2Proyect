using Domain.Models;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Order AddOrder(Order order);
        Order Purchase(Order order, string paymentMethod);
        List<Order> GetAllOrders();
        List<Order> GetOrderByUserId(Guid id);
        List<Order> GetOrdersByCustomerId(Guid id);
    }
}
