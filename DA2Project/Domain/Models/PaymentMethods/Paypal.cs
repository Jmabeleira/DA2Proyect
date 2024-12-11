using Domain.Interfaces;

namespace Domain.Models.PaymentMethods
{
    public class PayPal : PaymentMethod
    {
        public override string Type => "PayPal";
        public override void Accept(IPaymentMethodVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
