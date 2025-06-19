using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CourseSales.Payment.Api.Feature.Payment.Create
{
    public static class CreatePaymentEndPoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("", async (CreatePaymentCommand createPaymentCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createPaymentCommand);
                return result.ToGenericResult();
            })
                .WithName("create")
                .MapToApiVersion(1,0)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status502BadGateway);

            return group;
        }
    }
}
