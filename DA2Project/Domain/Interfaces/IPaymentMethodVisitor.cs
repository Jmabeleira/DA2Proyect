using Domain.Models.PaymentMethods;

namespace Domain.Interfaces
{
    public interface IPaymentMethodVisitor
    {
        void Visit(PayPal paypal);
        void Visit(Paganza paganza);
        void Visit(DebitCard debitCard);
        void Visit(CreditCard creditCard);
    }
}
