namespace Domain.Exceptions
{
    public class PaymentMethodNotFoundException : Exception
    {
        public PaymentMethodNotFoundException(string message) : base(message) { }
    }
}
