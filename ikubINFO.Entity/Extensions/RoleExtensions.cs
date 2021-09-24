using ikubINFO.DataAccess;
using ikubINFO.Entity.Dtos;
using System.Linq;

namespace ikubINFO.Entity.Extensions
{
    public static class RoleExtensions
    {
        public static Role AsEntity(this CreateRoleDto roleDto)
        {
            return new Role()
            {
                Title = roleDto.Title,
                Description = roleDto.Description
            };
        }

        public static Role AsUpdateEntity(this UpdateRoleDto roleDto, Role role)
        {
            role.Title = roleDto.Title;
            role.Description = roleDto.Description;
            return role;
        }

        public static RoleDto AsDto(this Role role)
        {
            return new RoleDto
            (
                role.RoleId,
                role.RoleGuid,
                role.Title,
                role.Description
            );
        }

        public static RoleWithUserDto AsRoleWithUserDto(this Role role)
        {
            return new RoleWithUserDto
            (
                role.RoleId,
                role.RoleGuid,
                role.Title,
                role.Description,
                role.Users.Select(x=>x.AsDto())
            );
        }
    }
}
