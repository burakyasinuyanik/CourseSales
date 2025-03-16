using CourseSales.Order.Application.Features.Orders.Create;
using CourseSales.Shared.Extensions;
using CourseSales.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseSales.Order.Api.EndPoints.Orders
{
    public static class CreateOrderEndPoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemPoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody]CreateOrderCommand request, [FromServices]IMediator mediator) =>
            {
                var result = await mediator.Send(request);

                result.ToGenericResult();
            })
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>()
                .WithName("CreateOrder")
                .MapToApiVersion(1,0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
