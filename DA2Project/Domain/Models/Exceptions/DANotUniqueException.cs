namespace Domain.Models.Exceptions
{
    public abstract class DANotUniqueException : Exception
    {
        public DANotUniqueException(string message) : base(message) { }
    }
}
