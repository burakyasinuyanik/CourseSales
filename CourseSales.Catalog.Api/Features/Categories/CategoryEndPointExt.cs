namespace CourseSales.Catalog.Api.Features.Categories
{
    public static class CategoryEndPointExt
    {
        public static void AddCategoryGroupEndPointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEndPoint();
        }
    }
}
