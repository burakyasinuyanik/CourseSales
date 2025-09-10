using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseSales.Payment.Api.Feature.Payment.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentsByUserIdGroupItemEndpoint(this RouteGroupBuilder group) {

            group.MapGet("", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllPaymentsByUserIdQuery());

                return result.ToGenericResult();
            })
                .WithName("get-all-payments-by-userid")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status204NoContent)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .RequireAuthorization("Password");

            return group;
        }
    }
}
