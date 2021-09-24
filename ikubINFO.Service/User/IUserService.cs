using ikubINFO.Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ikubINFO.Service.User
{
    public interface IUserService
    {
        Task<Status> AddUser(CreateUserDto userDto);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<Status> UpdateUser(string id, UpdateUserDto userDto);
        Task<UserDto> GetUserAsync(string id);
        Task<Status> DeleteUser(string id);
    }
}