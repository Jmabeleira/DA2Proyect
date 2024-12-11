namespace Domain.Exceptions
{
    public class InvalidPermissionException : Exception
    {
        public InvalidPermissionException() : base("The user does not have the required permission") { }
    }
}

