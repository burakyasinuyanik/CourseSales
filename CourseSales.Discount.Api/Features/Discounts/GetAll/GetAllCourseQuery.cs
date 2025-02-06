using AutoMapper;
using CourseSales.Discount.Api.Features.Discounts.Dtos;
using CourseSales.Discount.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CourseSales.Discount.Api.Features.Discounts.GetAll
{
    public record class GetAllCourseQuery:IRequestByServiceResult<List<DiscountDto>>;

    public class GetAllCourseQueryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<DiscountDto>>>
    {
        public async Task<ServiceResult<List<DiscountDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
           var discountList=await context.Discounts.ToListAsync();
            if(discountList is null)
                return ServiceResult<List<DiscountDto>>.Error("Herhangi bir indirim kodu bulunamadı",HttpStatusCode.NotFound);

            var discountListAsDto= mapper.Map<List<DiscountDto>>(discountList);
            return ServiceResult<List<DiscountDto>>.SuccessAsOk(discountListAsDto);
        }
    }

    public static class GetAllDiscountEndPoint
    {
        public static RouteGroupBuilder GetAllDiscountGroupItemEndPoint(this RouteGroupBuilder group) {

            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCourseQuery());

                return result.ToGenericResult();
            })
                .MapToApiVersion(1, 0)
                .WithName("GetAllDiscount");


            return group;
        }
    }
}
