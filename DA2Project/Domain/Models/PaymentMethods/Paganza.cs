using Domain.Interfaces;

namespace Domain.Models.PaymentMethods
{
    public class Paganza : PaymentMethod
    {
        public override string Type => "Paganza";
        public override void Accept(IPaymentMethodVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
