using AutoMapper;
using CourseSales.Discount.Api.Features.Discounts.Dtos;
using CourseSales.Discount.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;
using MongoDB.Driver;
using System.Net;

namespace CourseSales.Discount.Api.Features.Discounts.GetById
{
    public record class GetDiscountByIdQuery(Guid id):IRequestByServiceResult<DiscountDto>;


    public class GetDiscoundByIdHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetDiscountByIdQuery, ServiceResult<DiscountDto>>
    {
        public async Task<ServiceResult<DiscountDto>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
           var hasDiscount= await context.Discounts.FindAsync(new object?[] {request.id});
            if(hasDiscount is null)
                return ServiceResult<DiscountDto>.Error("Aramakta olduğunuz kod bulunamadı",HttpStatusCode.NotFound);

            var discountAsDto=mapper.Map<DiscountDto>(hasDiscount);

            return ServiceResult<DiscountDto>.SuccessAsOk(discountAsDto);
        }
    }

    public static class GetDiscountByIdGroupEndPoint
    {

        public static RouteGroupBuilder GetDiscountByIdGroupItemEndPoint(this RouteGroupBuilder group) {

            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new GetDiscountByIdQuery(id));
                return result.ToGenericResult();
            }).MapToApiVersion(1, 0)
            .WithName("GetDiscount");


            return group;
        }

    }
}
