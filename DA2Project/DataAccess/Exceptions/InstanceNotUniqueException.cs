using Domain.Models.Exceptions;

namespace DataAccess.Exceptions
{
    public class InstanceNotUniqueException : DANotUniqueException
    {
        public InstanceNotUniqueException(string message) : base(message) { }
    }
}
