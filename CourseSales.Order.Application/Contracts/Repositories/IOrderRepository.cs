using CourseSales.Order.Domain.Entities;

namespace CourseSales.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository:IGenericRepository<Guid,Domain.Entities.Order>
    {
        public Task<List<Order.Domain.Entities.Order>> GetOrderByUserId(Guid id);
        public Task SetStatus(string orderCode,Guid paymentId,OrderStatus status);
    }
}
