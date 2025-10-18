using CourseSales.Web.Pages.Instructor.Dto;
using Refit;

namespace CourseSales.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAync();
        [Get("/api/v1/courses/user/{userId}")]
        Task<ApiResponse<List<CourseDto>>> GetCourseByUserId(Guid UserId);

        [Multipart]
        [Post("/api/v1/courses")]
        Task<ApiResponse<Object>> CreateCourseAsync(
            [AliasAs("Name")] string Name,
            [AliasAs("Description")] string Description,
            [AliasAs("Price")] decimal Price,
            [AliasAs("Picture")] StreamPart? Picture,
            [AliasAs("CategoryId")] string CategoryId);


        [Put("/v1/catalog/courses")]
        Task<ApiResponse<Object>> UpdateCourseAsync(UpdateCourseRequest request);


        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<Object>> DeleteCourseAsync(Guid id);
    }
}
