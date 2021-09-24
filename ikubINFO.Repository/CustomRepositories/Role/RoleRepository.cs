using ikubINFO.DataAccess;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoleEntity = ikubINFO.DataAccess.Role;

namespace ikubINFO.Repository.CustomRepositories.Role
{
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(DbContext context, IUnitOfWork unitOfWork, IkubInfoDBContext ikubInfoDBContext) : base(context, unitOfWork, ikubInfoDBContext)
        {
        }

        public async Task<int> InsertRole(RoleEntity role)
        {
            this.Add(role);
            return await this._unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateRole(RoleEntity role)
        {
            this.Update(role);
            return await this._unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteRole(RoleEntity role)
        {
            this.Delete(role);
            return await this._unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteRoleWithUser(RoleEntity role)
        {
            foreach (var child in role.Users.ToList())
            {
                _ikubInfoDBContext.Users.Remove(child);
            }
            _ikubInfoDBContext.Roles.Remove(role);
            return await this._ikubInfoDBContext.SaveChangesAsync();
        }

        public async Task<RoleEntity> GetRole(string id)
        {
            return await this.FindAsync(x => x.RoleGuid == id && x.IsActive == true && x.IsDeleted == false);
        }

        public async Task<ICollection<RoleEntity>> GetRoles()
        {
            return await this.FindAllAsync(x => x.IsActive == true && x.IsDeleted == false);
        }

        public async Task<RoleEntity> GetRoleWithUser(string id)
        {
            return await _ikubInfoDBContext.Roles.Include(x => x.Users).FirstOrDefaultAsync(y => y.RoleGuid == id && y.IsActive == true && y.IsDeleted == false);
        }

        public async Task<ICollection<RoleEntity>> GetRolesWithUser()
        {
            return await _ikubInfoDBContext.Roles.Include(x => x.Users).Where(y => y.IsActive == true && y.IsDeleted == false).ToListAsync();
        }
    }
}
