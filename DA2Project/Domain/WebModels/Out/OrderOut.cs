using Domain.Models;
using Domain.Models.PaymentMethods;

namespace Domain.WebModels.Out
{
    public class OrderOut
    {
        public Guid Id { get; set; }
        public User Customer { get; set; }
        public IEnumerable<CartProduct> Products { get; set; }
        public DateTime Date { get; set; }
        public AppliedPromotion AppliedPromotion { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double TotalPrice { get; set; }

        public OrderOut(Order order)
        {
            Id = order.Id;
            Customer = order.Customer;
            Products = order.Products;
            Date = order.Date;
            AppliedPromotion = order.AppliedPromotion;
            PaymentMethod = order.PaymentMethod;
            TotalPrice = order.TotalPrice;
        }

        public override bool Equals(object obj)
        {
            if (obj is OrderOut orderOut)
            {
                return orderOut.Id == Id &&
                    orderOut.Customer.Equals(Customer)
                    && Products == orderOut.Products
                    && Date == orderOut.Date
                    && PaymentMethod == orderOut.PaymentMethod;
            }
            return false;
        }
    }
}
