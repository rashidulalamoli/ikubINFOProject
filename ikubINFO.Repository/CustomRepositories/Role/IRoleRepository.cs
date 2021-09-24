using System.Collections.Generic;
using System.Threading.Tasks;
using RoleEntity = ikubINFO.DataAccess.Role;

namespace ikubINFO.Repository.CustomRepositories.Role
{
    public interface IRoleRepository: IRepositoryBase<RoleEntity>
    {
        Task<int> DeleteRole(RoleEntity role);
        Task<int> DeleteRoleWithUser(RoleEntity role);
        Task<RoleEntity> GetRole(string id);
        Task<ICollection<RoleEntity>> GetRoles();
        Task<ICollection<RoleEntity>> GetRolesWithUser();
        Task<RoleEntity> GetRoleWithUser(string id);
        Task<int> InsertRole(RoleEntity role);
        Task<int> UpdateRole(RoleEntity role);
    }
}