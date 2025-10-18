using CourseSales.Web.Pages.Instructor.ViewModel;
using CourseSales.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseSales.Web.Pages.Instructor
{
    public class CourseModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> Courses { get; set; } = new();
        public async Task OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserId();
            if (result.IsFail) { }

            Courses= result.Data!;
        }
    }
}
