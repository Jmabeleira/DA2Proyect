using Domain.Models;

namespace Domain.WebModels.In
{
    public class AddUserIn
    {
        public string Email { get; set; }
        public IEnumerable<UserRole> UserRole { get; set; }
        public string Password { get; set; }
        public AddressIn Address { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = Email,
                UserRole = UserRole,
                Password = Password,
                Address = Address.ToEntity()
            };
        }
    }
}
