using Domain.Models;

namespace Domain.WebModels.Out
{
    public class UserOut
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserRole> UserRole { get; set; }
        public string Password { get; set; }
        public AddressOut Address { get; set; }
        public Guid Token { get; set; }
        public DateTime Timestamp { get; set; }

        public UserOut(User user)
        {
            Id = user.Id;
            Email = user.Email;
            UserRole = user.UserRole;
            Password = user.Password;
            Address = new AddressOut(user.Address);
            Timestamp = user.Timestamp;
            Token = user.Token;
        }

        public override bool Equals(object obj)
        {
            if (obj is UserOut userOut)
            {
                return Id == userOut.Id &&
                    Email == userOut.Email &&
                UserRole == userOut.UserRole &&
                Password == userOut.Password &&
                Address.Equals(userOut.Address) &&
                Token == userOut.Token &&
                Timestamp == userOut.Timestamp;
            }

            return false;
        }
    }
}
