using ikubINFO.Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ikubINFO.Service.Role
{
    public interface IRoleService
    {
        Task<Status> AddRole(CreateRoleDto roleDto);
        Task<Status> DeleteRole(string id);
        Task<Status> DeleteRoleWIthUser(string id);
        Task<RoleDto> GetRoleAsync(string id);
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        Task<IEnumerable<RoleWithUserDto>> GetRolesWithUserAsync();
        Task<RoleWithUserDto> GetRoleWithUserAsync(string id);
        Task<Status> UpdateRole(string id, UpdateRoleDto roleDto);
    }
}