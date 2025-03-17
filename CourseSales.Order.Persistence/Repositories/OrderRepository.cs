using CourseSales.Order.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
    {
        public Task<List<Domain.Entities.Order>> GetOrderByUserId(Guid id)
        {
            return Context.Orders.Include(x=>x.OrderItems).Where(x=>x.BuyerId == id).ToListAsync();
        }
    }
}
