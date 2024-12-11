using Domain.Models.PaymentMethods;

namespace Dtos.Models.PaymentMethods
{
    public class CreditCardDto : PaymentMethodDto
    {
        public CreditCardType CreditCardType { get; set; }
        public override string Type => "CreditCard" + CreditCardType.ToString() ?? string.Empty;

        public override PaymentMethod FromPaymentMethodDtoToPaymentMethod()
        {
            return new CreditCard
            {
                Id = Id,
                CardType = CreditCardType
            };
        }
    }
}
