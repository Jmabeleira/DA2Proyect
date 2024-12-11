using Domain.Models.PaymentMethods;

namespace Dtos.Models.PaymentMethods
{
    public abstract class PaymentMethodDto
    {
        public Guid Id { get; set; }
        public Guid OrderDtoId { get; set; }
        public OrderDto Order { get; set; }
        public abstract string Type { get; }

        public abstract PaymentMethod FromPaymentMethodDtoToPaymentMethod();
    }
}
