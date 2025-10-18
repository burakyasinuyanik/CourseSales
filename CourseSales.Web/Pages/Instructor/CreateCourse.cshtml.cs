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
            // Kategori listesini doldurmak için örnek kod
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.IsFail)
            {
                RedirectPermanent("/Error");
            }
            CreateCourseViewModel.SetCategoryDropdownList(categoriesResult.Data!);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Kategori listesini tekrar doldur
                var categories = new List<CategoryViewModel> {
                    new CategoryViewModel (Guid.NewGuid(),  "Software" ),
                    new CategoryViewModel (Guid.NewGuid(), "Design")
                };
                CreateCourseViewModel.SetCategoryDropdownList(categories);
                return Page();
            }
            // Kurs ekleme iþlemi burada yapýlacak
            // ...
            return RedirectToPage("/Index");
        }
    }
}
