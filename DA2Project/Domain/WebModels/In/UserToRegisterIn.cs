using Domain.Models;

namespace Domain.WebModels.In
{
    public class UserToRegisterIn
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public AddressIn Address { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = Email,
                Password = Password,
                UserRole = new List<UserRole> { UserRole.Client },
                Address = Address.ToEntity()
            };
        }
    }
}
