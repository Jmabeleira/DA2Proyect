using Domain.Models;

namespace Domain.WebModels.In
{
    public class AddressIn
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int DoorNumber { get; set; }

        public Address ToEntity()
        {
            return new Address
            {
                Id = Guid.NewGuid(),
                Country = Country,
                City = City,
                Street = Street,
                ZipCode = ZipCode,
                DoorNumber = DoorNumber
            };
        }
    }
}
