using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Dto
{
    public record class AddressDto(string Province,string District,string Street, string Zipcode,string Line);
    
}
