using AutoMapper;
using CourseSales.Discount.Api.Features.Discounts.Create;

namespace CourseSales.Discount.Api.Features.Discounts
{
    public class DiscountMapping:Profile
    {

        public DiscountMapping() {

            CreateMap<CreateDiscountCommand, Discount>();
        }
    }
}
