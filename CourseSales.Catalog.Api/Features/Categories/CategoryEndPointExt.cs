using CourseSales.Catalog.Api.Features.Categories.Delete;
using CourseSales.Catalog.Api.Features.Categories.GetAll;
using CourseSales.Catalog.Api.Features.Categories.GetById;

namespace CourseSales.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndPointExt(this WebApplication app)
        {
            app.MapGroup("api/categories")
                .WithTags("Category")
                .CreateCategoryGroupItemEndPoint()
                .GetAllCategoryGroupItemEndPoint()
                .GetByIdCategoryGroupItemEndPoint()
                .DeleteByIdCategoryGroupItemEndPoint();
        }
    }
}
