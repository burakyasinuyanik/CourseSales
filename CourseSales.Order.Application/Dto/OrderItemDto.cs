using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Dto
{
    public record class OrderItemDto(Guid ProductId,string ProductName,decimal UnitPrice);
    
}
