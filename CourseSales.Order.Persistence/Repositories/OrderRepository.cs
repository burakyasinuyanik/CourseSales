using CourseSales.Order.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context): GenericRepository<Guid, Domain.Entities.Order>(context),IOrderRepository 
    {
    }
}
