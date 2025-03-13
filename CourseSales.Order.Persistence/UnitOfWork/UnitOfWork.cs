using CourseSales.Order.Application.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public void BeginTransactionAsync()
        {
            context.Database.BeginTransaction();
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken=default)
        {
            return context.Database.CommitTransactionAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
        {
            return context.SaveChangesAsync(cancellationToken);

        }
    }
}
