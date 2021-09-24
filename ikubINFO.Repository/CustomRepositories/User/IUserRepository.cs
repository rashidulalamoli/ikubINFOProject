using System.Collections.Generic;
using System.Threading.Tasks;
using UserEntity = ikubINFO.DataAccess.User;

namespace ikubINFO.Repository.CustomRepositories.User
{
    public interface IUserRepository: IRepositoryBase<UserEntity>
    {
        Task<UserEntity> GetUser(string id);
        Task<UserEntity> GetUserByEmail(string email);
        Task<ICollection<UserEntity>> GetUsers();
        Task<int> InsertUser(UserEntity user);
        Task<int> UpdateUser(UserEntity user);
        Task<int> DeleteUser(UserEntity user);
    }
}