using Domain.Interfaces;

namespace Domain.Models.PaymentMethods
{
    public abstract class PaymentMethod
    {
        public Guid Id { get; set; }
        public abstract string Type { get; }
        public abstract void Accept(IPaymentMethodVisitor visitor);

        public override bool Equals(object? obj)
        {
            if (obj is PaymentMethod paymentMethod)
            {
                return paymentMethod.Id == Id;
            }
            return false;
        }
    }
}
