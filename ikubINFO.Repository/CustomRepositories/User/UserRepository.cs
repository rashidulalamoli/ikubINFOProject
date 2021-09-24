using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.GenericRepository;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = ikubINFO.DataAccess.User;

namespace ikubINFO.Repository.CustomRepositories.User
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        { }

        public async Task<int> InsertUser(UserEntity user)
        {
            this.Add(user);
            return await this._unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateUser(UserEntity user)
        {
            this.Update(user);
            return await this._unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteUser(UserEntity user)
        {
            this.Delete(user);
            return await this._unitOfWork.SaveAsync();
        }

        public async Task<UserEntity> GetUser(string id)
        {
            return await this.FindAsync(x => x.UserGuid == id && x.IsActive == true && x.IsDeleted == false);
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await this.FindAsync(x => x.Email == email && x.IsActive == true && x.IsDeleted == false);
        }

        public async Task<ICollection<UserEntity>> GetUsers()
        {
            return await this.FindAllAsync(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
