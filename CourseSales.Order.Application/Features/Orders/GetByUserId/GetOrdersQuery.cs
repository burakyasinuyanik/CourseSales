using CourseSales.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders.GetByUserId
{
    public record class GetOrdersQuery:IRequestByServiceResult<List<GetOrderResponse>>;
    
}
