using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Dto
{
    public record class PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);
    
}
