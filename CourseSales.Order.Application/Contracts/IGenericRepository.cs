using System.Linq.Expressions;

namespace CourseSales.Order.Application.Contracts
{
    public interface IGenericRepository<TId,TEntity> where TEntity : class where TId : struct
    {
        public Task<bool> AnyAsync(TId id);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> peridecate);
        public List<TEntity> GetAllAsync();
        public List<TEntity> GetAllPagedAsync(int pageNumber,int pageSize);
        ValueTask<TEntity> GetAsync(TId id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
