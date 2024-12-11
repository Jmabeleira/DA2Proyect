namespace Logic.Exceptions
{
    public class NoStockException : Exception
    {
        public NoStockException(string message) : base(message) { }

        public NoStockException(string message, Exception inner) : base(message, inner) { }
    }
}
