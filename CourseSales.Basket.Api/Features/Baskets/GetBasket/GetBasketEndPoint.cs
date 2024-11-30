
using CourseSales.Shared.Extensions;
using MediatR;

namespace CourseSales.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketEndPoint
    {
        public static RouteGroupBuilder GetBasketItemGroupItem(this RouteGroupBuilder group)
        {

            group.MapGet("/user", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetBasketItemQuery());
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0);

            return group;
        }
    }
}
