using Asp.Versioning.Builder;
using CourseSales.Catalog.Api.Features.Categories.Create;
using CourseSales.Catalog.Api.Features.Categories.Delete;
using CourseSales.Catalog.Api.Features.Categories.GetAll;
using CourseSales.Catalog.Api.Features.Categories.GetById;

namespace CourseSales.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndPointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories")
                .WithTags("Category")
                .CreateCategoryGroupItemEndPoint()
                .GetAllCategoryGroupItemEndPoint()
                .GetByIdCategoryGroupItemEndPoint()
                .DeleteByIdCategoryGroupItemEndPoint()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
