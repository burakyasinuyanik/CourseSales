using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseSales.Web.Pages.Instructor.ViewModel
{
    public record class CreateCourseViewModel
    {
        public static CreateCourseViewModel Empty => new();


        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public Guid? CategoryId { get; init; }
        public SelectList CategoryDropdownList { get; set; } = null!;


        [Display(Name = "Kurs Resmi")] public IFormFile? PictureFormFile { get; init; }


        [Display(Name = "Kurs Adı")]
        [Required(ErrorMessage = "Kurs adı zorunludur.")]
        public string Name { get; init; } = null!;


        [Display(Name = "Kurs Açıklaması")]
        [Required(ErrorMessage = "Kurs açıklaması zorunludur.")]
        public string Description { get; init; } = null!;


        [Display(Name = "Kurs Fiyatı")]
        [Required(ErrorMessage = "Kurs fiyatı zorunludur.")]
        public decimal Price { get; init; }

        public void SetCategoryDropdownList(List<CategoryViewModel> categories)
        {
            CategoryDropdownList = new SelectList(categories, "Id", "Name");
        }
    }
}
