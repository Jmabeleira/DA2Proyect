namespace Logic.Exceptions
{
    public class CurrentUserException : Exception
    {
        public CurrentUserException(string message, Exception ex) : base(message, ex) { }
    }
}
