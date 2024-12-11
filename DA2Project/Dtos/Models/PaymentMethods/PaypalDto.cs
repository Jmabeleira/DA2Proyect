using Domain.Models.PaymentMethods;

namespace Dtos.Models.PaymentMethods
{
    public class PaypalDto : PaymentMethodDto
    {
        public override string Type => "Paypal";
        public override PaymentMethod FromPaymentMethodDtoToPaymentMethod()
        {
            return new PayPal();
        }
    }
}
