using Domain.Exceptions;

namespace Domain.Models.PaymentMethods
{
    public static class PaymentHelper
    {
        public static Dictionary<string, PaymentMethod> PaymentMethods
        {
            get
            {
                return new Dictionary<string, PaymentMethod>
                {
                    { PaymentKey.CreditCardVisa, new CreditCard() { CardType=CreditCardType.Visa } },
                    { PaymentKey.CreditCardMasterCard, new CreditCard() { CardType=CreditCardType.MasterCard } },
                    { PaymentKey.DebitCardSantander, new DebitCard() { CardType=DebitCardType.Santander } },
                    { PaymentKey.DebitCardItau, new DebitCard() { CardType=DebitCardType.Itau } },
                    { PaymentKey.DebitCardBbva, new DebitCard() { CardType=DebitCardType.Bbva } },
                    { PaymentKey.PayPal, new PayPal() },
                    { PaymentKey.Paganza, new Paganza() }
                };
            }
        }

        public static PaymentMethod GetPaymentMethodInstance(string paymentKey)
        {
            try
            {
                return PaymentMethods[paymentKey];
            }
            catch (Exception)
            {
                throw new PaymentMethodNotFoundException("Payment method not found");
            }
        }
    }
}
