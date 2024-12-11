namespace Logic.Exceptions
{
    public class UnhandledLogicException : Exception
    {
        public UnhandledLogicException(Exception inner) : base("An unexpected error has ocurred: " + inner.Message, inner) { }
    }
}
