namespace Dtos.Models
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int DoorNumber { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}
