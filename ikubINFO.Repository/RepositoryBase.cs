using ikubINFO.DataAccess;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.GenericRepository;
using ikubINFO.Repository.GenericRepositoryAndUnitOfWork.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ikubINFO.Repository
{
    public interface IRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
    public class RepositoryBase<TEntity> : Repository<TEntity>, IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IkubInfoDBContext _ikubInfoDBContext;
        public RepositoryBase(DbContext context) : base(context)
        {
            this._context = context;
        }

        public RepositoryBase(DbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            this._context = context;
            _unitOfWork = unitOfWork;
        }

        public RepositoryBase(DbContext context, IUnitOfWork unitOfWork, IkubInfoDBContext ikubInfoDBContext) : base(context)
        {
            this._context = context;
            _unitOfWork = unitOfWork;
            _ikubInfoDBContext = ikubInfoDBContext;
        }

    }
}