using CourseSales.Web.Pages.Instructor.ViewModel;
using CourseSales.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseSales.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        [BindProperty]
        public CreateCourseViewModel CreateCourseViewModel { get; set; } = CreateCourseViewModel.Empty;

        public async Task OnGet()
        {
           
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.IsFail)
            {
                RedirectPermanent("/Error");
            }
            CreateCourseViewModel.SetCategoryDropdownList(categoriesResult.Data!);
        }

        public async Task<IActionResult> OnPostAsync()
        {
         
            var createCourseResult = await catalogService.CreateCourseAsync(CreateCourseViewModel);
            if (createCourseResult.IsFail)
            {
                
            }

            return RedirectToPage("Courses");
        }
    }
}
