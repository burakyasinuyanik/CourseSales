using CourseSales.Order.Application.Features.Orders.GetByUserId;
using CourseSales.Shared.Extensions;
using MediatR;

namespace CourseSales.Order.Api.EndPoints.Orders
{
    public static class GetOrderEndPoint
    {
        public static RouteGroupBuilder GetOrderGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetOrdersQuery());

                return result.ToGenericResult();
            })
                .WithName("GetOrders")

                .MapToApiVersion(1,0)
                ;

            return group;
        }
    }
}
