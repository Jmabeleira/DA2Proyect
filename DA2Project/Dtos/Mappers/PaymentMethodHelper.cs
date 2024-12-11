using Domain.Models.PaymentMethods;
using Dtos.Models.PaymentMethods;

namespace Dtos.Mappers
{
    public class PaymentMethodHelper
    {
        public static PaymentMethodDto FromPaymentToPaymentDto(PaymentMethod paymentMethod)
        {
            var visitor = new PaymentMethodVisitor();
            paymentMethod.Accept(visitor);
            return visitor.GetPaymentMethodDto();
        }
    }
}
