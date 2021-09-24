using System;
using System.Threading.Tasks;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.GenericRepository;

namespace ikubINFO.Repository.GenericRepositoryAndUnitOfWork.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        void Save();
        Task<int> SaveAsync();
        IDisposable BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}