using CourseSales.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CourseSales.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndPoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndPoint(this RouteGroupBuilder group)
        {

            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<CreateCourseCommand>>()
            .WithName("Create Course")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .MapToApiVersion(1, 0);
            

            return group;
        }
    }
}
