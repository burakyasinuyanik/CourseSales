using CourseSales.Shared;
using MediatR;

namespace CourseSales.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;

}
