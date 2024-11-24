using CourseSales.Catalog.Api.Features.Categories.Create;
using CourseSales.Shared.Filters;

namespace CourseSales.Catalog.Api.Features.Categories
{
    public static class CreateCategoryEndPoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndPoint(this RouteGroupBuilder group)
        {

            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return result.ToGenericResult();

                }).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>()
                .WithName("Create Category")
                .MapToApiVersion(1,0);
            

            return group;
        }
    }
}
