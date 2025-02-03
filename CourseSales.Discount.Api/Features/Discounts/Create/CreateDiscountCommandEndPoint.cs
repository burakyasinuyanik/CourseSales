using CourseSales.Shared.Extensions;
using CourseSales.Shared.Filters;
using MediatR;

namespace CourseSales.Discount.Api.Features.Discounts.Create
{
    public static class CreateDiscountCommandEndPoint
    {

        public static RouteGroupBuilder CreateDiscountGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand request, IMediator mediator) =>
            {
                var result = await mediator.Send(request);
                return result.ToGenericResult();
            })
              .WithName("CreateDiscount")
              .MapToApiVersion(1.0)
              .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();
                

            return group;
        }
    }
}
