using ikubINFO.Entity.Dtos;
using ikubINFO.Entity.Extensions;
using ikubINFO.Repository.CustomRepositories.User;
using ikubINFO.Utility.PasswordHelper;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserEntity = ikubINFO.DataAccess.User;

namespace ikubINFO.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _hasher;
        public UserService(IUserRepository repository, IPasswordHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }

        public async Task<Status> AddUser(CreateUserDto userDto)
        {
            if (await CheckUserExist(userDto.Email) == false)
            {
                UserEntity user = userDto.AsEntity();
                string hashValue = _hasher.Hash(userDto.Password);
                user.PasswordHash = hashValue;
                user.UserGuid = Guid.NewGuid().ToString();
                user.CreatedDate = DateTime.UtcNow;
                user.ModifiedDate = DateTime.UtcNow;
                user.IsActive = true;
                user.IsDeleted = false;
                if (await _repository.InsertUser(user) > 0)
                {
                    return new(StatusCodes.Status200OK, StaticData.USER_ADD_SUCCESS);
                }
                else
                {
                    return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
                }
            }
            else
            {
                return new(StatusCodes.Status409Conflict, StaticData.USER_EXIST);
            }
        }

        public async Task<Status> UpdateUser(string id, UpdateUserDto userDto)
        {
            UserEntity user = await _repository.GetUser(id);
            if (user is null)
            {
                return new(StatusCodes.Status404NotFound, StaticData.USER_NOT_FOUND);
            }

            if (user.Email == userDto.Email)
            {
                user = userDto.AsUpdateEntity(user);
            }
            else
            {
                if (await CheckUserExist(userDto.Email) == false)
                {
                    user = userDto.AsUpdateEntity(user);
                }
                else
                {
                    return new(StatusCodes.Status409Conflict, StaticData.USER_EMAIL_EXIST);
                }
            }

            string hashValue = _hasher.Hash(userDto.Password);
            user.PasswordHash = hashValue;
            user.ModifiedDate = DateTime.UtcNow;
            if (await _repository.UpdateUser(user) > 0)
            {
                return new(StatusCodes.Status200OK, StaticData.USER_UPDATE_SUCCESS);
            }
            else
            {
                return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
            }

        }

        private async Task<bool> CheckUserExist(string email)
        {
            UserEntity existinguser = await _repository.GetUserByEmail(email);
            if (existinguser is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = (await _repository.GetUsers()).Select(user => user.AsDto());
            return await Task.FromResult(users);
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            return (await _repository.GetUser(id)).AsDto();
        }

        public async Task<Status> DeleteUser(string id)
        {
            UserEntity user = await _repository.GetUser(id);
            if (user is null)
            {
                return new(StatusCodes.Status404NotFound, StaticData.USER_NOT_FOUND);
            }

            if (await _repository.DeleteUser(user) > 0)
            {
                return new(StatusCodes.Status200OK, StaticData.USER_DELETE_SUCCESS);
            }
            else
            {
                return new(StatusCodes.Status400BadRequest, StaticData.ERROR_TYPE_NONE);
            }
        }
    }
}