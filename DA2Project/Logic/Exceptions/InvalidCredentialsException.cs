namespace Logic.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message, Exception ex) : base(message, ex) { }
    }
}
