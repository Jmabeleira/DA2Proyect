using Domain.Models;
using Dtos.Models;

namespace Dtos.Mappers
{
    public static class AddressHelper
    {
        public static AddressDto FromAddressToAddressDto(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Country = address.Country,
                City = address.City,
                Street = address.Street,
                ZipCode = address.ZipCode,
                DoorNumber = address.DoorNumber
            };
        }

        public static Address FromAddressDtoToAddress(AddressDto addressDto)
        {
            return new Address
            {
                Id = addressDto.Id,
                Country = addressDto.Country,
                City = addressDto.City,
                Street = addressDto.Street,
                ZipCode = addressDto.ZipCode,
                DoorNumber = addressDto.DoorNumber
            };
        }
    }
}
