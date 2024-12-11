namespace Logic.Exceptions
{
    public class NoContentException : Exception
    {
        public NoContentException(string message, Exception inner) : base(message, inner) { }
    }
}
