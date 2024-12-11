using Domain.Models;
using Dtos.Models;

namespace Dtos.Mappers
{
    public static class UserHelper
    {
        public static User FromUserDtoToUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                UserRole = userDto.UserRole,
                Password = userDto.Password,
                Address = AddressHelper.FromAddressDtoToAddress(userDto.Address),
                Token = userDto.Token,
                Timestamp = userDto.Timestamp
            };
        }

        public static UserDto FromUserToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserRole = user.UserRole.ToList(),
                Password = user.Password,
                Address = AddressHelper.FromAddressToAddressDto(user.Address),
                Token = user.Token,
                Timestamp = user.Timestamp
            };
        }
    }
}
