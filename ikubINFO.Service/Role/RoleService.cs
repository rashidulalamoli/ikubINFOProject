using ikubINFO.Entity.Dtos;
using ikubINFO.Entity.Extensions;
using ikubINFO.Repository.CustomRepositories.Role;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleEntity = ikubINFO.DataAccess.Role;

namespace ikubINFO.Service.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Status> AddRole(CreateRoleDto roleDto)
        {
            RoleEntity role = roleDto.AsEntity();
            role.RoleGuid = Guid.NewGuid().ToString();
            role.CreatedDate = DateTime.UtcNow;
            role.ModifiedDate = DateTime.UtcNow;
            role.IsActive = true;
            role.IsDeleted = false;
            if (await _repository.InsertRole(role) > 0)
            {
                return new(StatusCodes.Status200OK, StaticData.ROLE_ADD_SUCCESS);
            }
            else
            {
                return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
            }
        }

        public async Task<Status> UpdateRole(string id, UpdateRoleDto roleDto)
        {
            RoleEntity role = await _repository.GetRole(id);
            if (role is null)
            {
                return new(StatusCodes.Status404NotFound, StaticData.ROLE_NOT_FOUND);
            }

            role = roleDto.AsUpdateEntity(role);
            role.ModifiedDate = DateTime.UtcNow;
            _repository.Update(role);
            if (await _repository.UpdateRole(role) > 0)
            {
                return new(StatusCodes.Status200OK, StaticData.ROLE_UPDATE_SUCCESS);
            }
            else
            {
                return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
            }

        }

        public async Task<Status> DeleteRole(string id)
        {
            RoleEntity role = await _repository.GetRoleWithUser(id);
            if (role is null)
            {
                return new(StatusCodes.Status404NotFound, StaticData.ROLE_NOT_FOUND);
            }

            if (role.Users.Count() > 0)
            {
                return new(StatusCodes.Status409Conflict, StaticData.CHILD_FOUND);
            }

            if (await _repository.DeleteRole(role) > 0)
            {
                return new(StatusCodes.Status200OK, StaticData.ROLE_DELETE_SUCCESS);
            }
            else
            {
                return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
            }
        }

        public async Task<Status> DeleteRoleWIthUser(string id)
        {
            RoleEntity role = await _repository.GetRoleWithUser(id);
            if (role is null)
            {
                return new(StatusCodes.Status404NotFound, StaticData.ROLE_NOT_FOUND);
            }

            if (await _repository.DeleteRoleWithUser(role) > 0)
            {
                return new(StatusCodes.Status200OK, StaticData.ROLE_DELETE_SUCCESS);
            }
            else
            {
                return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
            }
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = (await _repository.GetRoles()).Select(role => role.AsDto());
            return roles;
        }

        public async Task<RoleDto> GetRoleAsync(string id)
        {
            return (await _repository.GetRole(id)).AsDto();
        }

        public async Task<IEnumerable<RoleWithUserDto>> GetRolesWithUserAsync()
        {
            var roles = (await _repository.GetRolesWithUser()).Select(role => role.AsRoleWithUserDto());
            return roles;
        }

        public async Task<RoleWithUserDto> GetRoleWithUserAsync(string id)
        {
            return (await _repository.GetRoleWithUser(id)).AsRoleWithUserDto();
        }

    }
}
