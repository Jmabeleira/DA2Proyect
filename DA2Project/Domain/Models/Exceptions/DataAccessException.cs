namespace Domain.Models.Exceptions
{
    public abstract class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception inner) : base(message, inner) { }
    }
}
