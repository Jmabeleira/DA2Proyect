using Domain.Interfaces;
using Domain.Models.PaymentMethods;
using Dtos.Models.PaymentMethods;

namespace Dtos.Mappers
{
    public class PaymentMethodVisitor : IPaymentMethodVisitor
    {
        private PaymentMethodDto _paymentMethodDto;
        public void Visit(PayPal paypal)
        {
            _paymentMethodDto = new PaypalDto() { Id = paypal.Id };
        }

        public void Visit(Paganza paganza)
        {
            _paymentMethodDto = new PaganzaDto() { Id = paganza.Id };
        }

        public void Visit(DebitCard debitCard)
        {
            _paymentMethodDto = new DebitCardDto
            {
                Id = debitCard.Id,
                DebitCardType = debitCard.CardType
            };
        }

        public void Visit(CreditCard creditCard)
        {
            _paymentMethodDto = new CreditCardDto
            {
                Id = creditCard.Id,
                CreditCardType = creditCard.CardType
            };
        }

        public PaymentMethodDto GetPaymentMethodDto()
        {
            return _paymentMethodDto;
        }
    }
}
