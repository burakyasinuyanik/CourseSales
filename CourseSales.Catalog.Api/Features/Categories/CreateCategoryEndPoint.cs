using CourseSales.Catalog.Api.Features.Categories.Create;
using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

            });

            return group;
        }
    }
}
