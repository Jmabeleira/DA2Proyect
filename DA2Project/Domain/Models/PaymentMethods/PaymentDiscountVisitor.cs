using Domain.Interfaces;

namespace Domain.Models.PaymentMethods
{
    public class PaymentDiscountVisitor : IPaymentMethodVisitor, IPaymentDiscountVisitor
    {
        private double _totalDiscount;

        public double GetDiscount()
        {
            return _totalDiscount;
        }

        public void Visit(PayPal paypal)
        {
            _totalDiscount = 0;
        }

        public void Visit(Paganza paganza)
        {
            _totalDiscount = 0.1;
        }

        public void Visit(DebitCard debitCard)
        {
            _totalDiscount = 0;
        }

        public void Visit(CreditCard creditCard)
        {
            _totalDiscount = 0;
        }
    }
}
