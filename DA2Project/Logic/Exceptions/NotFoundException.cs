namespace Logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
