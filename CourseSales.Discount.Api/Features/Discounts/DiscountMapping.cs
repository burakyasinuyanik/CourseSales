using AutoMapper;
using CourseSales.Discount.Api.Features.Discounts.Create;
using CourseSales.Discount.Api.Features.Discounts.Dtos;

namespace CourseSales.Discount.Api.Features.Discounts
{
    public class DiscountMapping:Profile
    {

        public DiscountMapping() {

            CreateMap<CreateDiscountCommand, Discount>();
            CreateMap<Discount, DiscountDto>();
        }
    }
}
