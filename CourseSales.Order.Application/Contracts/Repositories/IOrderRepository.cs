﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository:IGenericRepository<Guid,Domain.Entities.Order>
    {
        public Task<List<Order.Domain.Entities.Order>> GetOrderByUserId(Guid id);
    }
}
