namespace Logic.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
