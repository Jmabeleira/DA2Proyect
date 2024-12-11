using Domain.Models;

namespace Dtos.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<UserRole> UserRole { get; set; }
        public string Password { get; set; }
        public AddressDto Address { get; set; }
        public Guid Token { get; set; }
        public DateTime Timestamp { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
    }
}
