using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Contracts.Refit.PaymentService
{
    public record class GetPaymentStatusResponse(bool IsPaid, Guid? PaymentId);
    
}
