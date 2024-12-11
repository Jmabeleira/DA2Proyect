using Domain.Models.PaymentMethods;

namespace Dtos.Models.PaymentMethods
{
    public class DebitCardDto : PaymentMethodDto
    {
        public DebitCardType DebitCardType { get; set; }

        public override string Type => "CreditCard" + DebitCardType.ToString() ?? string.Empty;

        public override PaymentMethod FromPaymentMethodDtoToPaymentMethod()
        {
            return new DebitCard
            {
                Id = Id,
                CardType = DebitCardType
            };
        }
    }
}
