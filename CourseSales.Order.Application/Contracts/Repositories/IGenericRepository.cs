using CourseSales.Order.Domain.Entities;
using System.Linq.Expressions;

namespace CourseSales.Order.Application.Contracts.Repositories
{
    public interface IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
    {
        public Task<bool> AnyAsync(TId id);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> peridecate);
        public List<TEntity> GetAllAsync();
        public Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);
        ValueTask<TEntity?> GetByIdAsync(TId id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);


    }
}
