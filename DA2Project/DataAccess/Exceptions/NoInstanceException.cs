using Domain.Models.Exceptions;

namespace DataAccess.Exceptions
{
    public class NoInstanceException : DANoInstanceException
    {
        public NoInstanceException(string message) : base(message) { }
    }
}
