using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPurchaseService
    {
        Order Purchase(Order order, string paymentMethod);
    }
}
