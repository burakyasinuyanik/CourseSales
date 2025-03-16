using CourseSales.Shared.Extensions;
using CourseSales.Shared.Filters;
using MediatR;

namespace CourseSales.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndPoint
    {
        public static RouteGroupBuilder AddBasketGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);

                    return result.ToGenericResult();

                })
                .MapToApiVersion(1, 0)
                .WithName("AddBasketItem")
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();
                


            return group;
        }
    }
}
