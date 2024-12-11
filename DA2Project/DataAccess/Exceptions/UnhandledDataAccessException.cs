using Domain.Models.Exceptions;

namespace DataAccess.Exceptions
{
    public class UnhandledDataAccessException : DataAccessException
    {
        public UnhandledDataAccessException(Exception inner) : base("A DataAccess error has ocurred: " + inner.Message, inner) { }
    }
}
