using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ikubINFO.Entity.Dtos
{
    public record RoleDto(int Id, string RoleGuid, string Title, string Description);
    public record RoleWithUserDto(int Id, string RoleGuid, string Title, string Description, IEnumerable<UserDto> Users);
    public record CreateRoleDto([Required] string Title, string Description);
    public record UpdateRoleDto([Required] string Title, string Description);
}