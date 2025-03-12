using CourseSales.Order.Application.Dto;
using CourseSales.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders.Create
{
    public record class CreateOrderCommand(float? discountRate,AddressDto Address,PaymentDto Payment,List<OrderItemDto> Items):IRequestByServiceResult;
    
}
