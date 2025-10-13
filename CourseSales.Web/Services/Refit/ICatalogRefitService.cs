using CourseSales.Web.Pages.Instructor.Dto;
using Refit;

namespace CourseSales.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Get("/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAync();


        [Post("/v1/catalog/courses")]
        Task<ApiResponse<Object>> CreateCourseAsync(CreateCourseRequest request);


        [Put("/v1/catalog/courses")]
        Task<ApiResponse<Object>> UpdateCourseAsync(UpdateCourseRequest request);


        [Delete("/v1/catalog/courses/{id}")]
        Task<ApiResponse<Object>> DeleteCourseAsync(Guid id);
    }
}
