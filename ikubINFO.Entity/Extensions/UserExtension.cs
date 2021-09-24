using ikubINFO.DataAccess;
using ikubINFO.Entity.Dtos;

namespace ikubINFO.Entity.Extensions
{
    public static class UserExtension
    {
        public static User AsEntity(this CreateUserDto userDto)
        {
            return new User()
            {
                RoleId = userDto.RoleId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Initials = userDto.Initials,
                FullName = userDto.FullName,
                Email = userDto.Email,
                UserName = userDto.UserName,
                Phone = userDto.Phone,
                Image = userDto.Image
            };
        }

        public static User AsUpdateEntity(this UpdateUserDto userDto, User user)
        {
            user.RoleId = userDto.RoleId;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Initials = userDto.Initials;
            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.UserName = userDto.UserName;
            user.Phone = userDto.Phone;
            user.Image = userDto.Image;
            return user;
        }

        public static UserDto AsDto(this User user)
        {
            return new UserDto
            (
                user.UserGuid,
                user.FirstName,
                user.LastName,
                user.Initials,
                user.FullName,
                user.Email,
                user.UserName,
                user.Phone,
                user.Image,
                user.RoleId
            );
        }
    }
}
