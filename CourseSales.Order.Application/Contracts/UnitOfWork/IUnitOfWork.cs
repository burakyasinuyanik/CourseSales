using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
        void BeginTransactionAsync();
        Task CommitTransactionAsync(CancellationToken cancellationToken=default);

    }
}
