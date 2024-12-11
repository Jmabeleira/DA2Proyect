using Domain.Models;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        Order AddOrder(Order entity);
        List<Order> GetAllOrders();
    }
}
