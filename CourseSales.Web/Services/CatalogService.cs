using CourseSales.Web.Pages.Instructor.ViewModel;
using CourseSales.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;


namespace CourseSales.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
    {
        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesAync();

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Error.GetContentAsAsync<ProblemDetails>();
                logger.LogError("Error fetching categories: {StatusCode} - {Title}: {Detail}", response.StatusCode, problemDetails?.Title, problemDetails?.Detail);
                return ServiceResult<List<CategoryViewModel>>.Error(problemDetails!, response.StatusCode);
            }

            var categories = response.Content?
                .Select(c => new CategoryViewModel(c.Id, c.Name))
                .ToList();
            return ServiceResult<List<CategoryViewModel>>.Success(categories!);
        }
        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
        {
            StreamPart? pictureStreamPart = null;
            await using var stream = model.PictureFormFile?.OpenReadStream();

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            {
               
                pictureStreamPart = new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);

            }
            var response = await catalogRefitService.CreateCourseAsync(model.Name, model.Description, model.Price, pictureStreamPart, model.CategoryId!.ToString()
               );

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
            }
            return ServiceResult.Success();
        }
    }
}
