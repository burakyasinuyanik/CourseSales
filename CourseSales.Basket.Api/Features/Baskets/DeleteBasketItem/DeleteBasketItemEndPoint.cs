using CourseSales.Shared.Extensions;
using CourseSales.Shared.Filters;
using FluentValidation;
using MediatR;

namespace CourseSales.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndPoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupEndPoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteBasketItemCommand(id));

                return result.ToGenericResult();
            })
            .AddEndpointFilter<ValidationFilter<DeleteBasketItemCommand>>()
            .MapToApiVersion(1,0);

            return group;
        }
    }
}
