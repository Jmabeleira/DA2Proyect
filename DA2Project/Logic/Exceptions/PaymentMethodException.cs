namespace Logic.Exceptions
{
    public class PaymentMethodException : Exception
    {
        public PaymentMethodException(string message, Exception inner) : base(message, inner) { }
    }
}
