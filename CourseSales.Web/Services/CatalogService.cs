using CourseSales.Web.Pages.Instructor.ViewModel;
using CourseSales.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;

namespace CourseSales.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService,ILogger<CatalogService> logger)
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
    }
}
