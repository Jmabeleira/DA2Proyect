namespace Domain.Models.Exceptions
{
    public abstract class DANoInstanceException : Exception
    {
        public DANoInstanceException(string message) : base(message) { }
    }
}
