using Domain.Models;

namespace Domain.WebModels.Out
{
    public class AddressOut
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int DoorNumber { get; set; }

        public AddressOut(Address address)
        {
            Country = address?.Country;
            City = address?.City;
            Street = address?.Street;
            ZipCode = address?.ZipCode;
            DoorNumber = address?.DoorNumber ?? 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is AddressOut addressOut)
            {
                return Country == addressOut?.Country &&
                    City == addressOut?.City &&
                    Street == addressOut?.Street &&
                    ZipCode == addressOut?.ZipCode &&
                    DoorNumber == addressOut?.DoorNumber;
            }
            return false;
        }
    }
}
