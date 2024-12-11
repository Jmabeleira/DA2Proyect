using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    public class User
    {
        private string _email;
        private string _password;
        private Guid _id;

        public Guid Id
        {
            get
            {
                return GetId();
            }
            set { _id = value; }
        }

        private Guid GetId()
        {
            if (_id == Guid.Empty)
            {
                _id = Guid.NewGuid();
            }
            return _id;
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = IsEmailValid(value) ? value :
                     throw new RequestValidationException("The provided email address is not valid");
            }
        }
        public IEnumerable<UserRole> UserRole { get; set; }
        public string Password { get { return _password; } set { _password = value; } }
        public Address Address { get; set; }
        public Guid Token { get; set; }
        public DateTime Timestamp { get; set; }

        private bool IsEmailValid(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        public override bool Equals(object? obj)
        {
            if (obj is User u)
            {
                return u.Id == Id &&
                       u.Email == Email &&
                       u.Password == Password &&
                       u.Address.Equals(Address) &&
                       u.UserRole.SequenceEqual(UserRole);
            }
            return false;
        }
    }
}
