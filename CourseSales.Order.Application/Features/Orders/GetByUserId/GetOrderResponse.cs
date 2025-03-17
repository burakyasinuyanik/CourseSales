using CourseSales.Order.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders.GetByUserId
{
    public record class GetOrderResponse(DateTime OrderDate,decimal TotalPrice,List<OrderItemDto> Items);
    
}
