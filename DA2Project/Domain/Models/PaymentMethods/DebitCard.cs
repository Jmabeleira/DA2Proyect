using Domain.Interfaces;

namespace Domain.Models.PaymentMethods
{
    public class DebitCard : PaymentMethod
    {
        public DebitCardType CardType { get; set; }
        public override string Type => "CreditCard" + CardType.ToString() ?? string.Empty;

        public override void Accept(IPaymentMethodVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
