using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseSales.Payment.Api.Feature.Payment.GetStatus
{
    public static class GetPaymentStatusQueryEndPoint
    {
        public static RouteGroupBuilder GetPaymentStatusGroupItemQueryEndPoint(this RouteGroupBuilder group) {

            group.MapGet("/status/{orderCode}", async ([FromServices]IMediator mediator, string orderCode) =>
            {
                var result = await mediator.Send(new GetPaymentStatusQuery(orderCode));
                return result.ToGenericResult();
            })
                .WithName("GetPaymentStatus")
                .MapToApiVersion(1,0)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .RequireAuthorization("ClientCredential");

            return group;
        
        }
    }
}
