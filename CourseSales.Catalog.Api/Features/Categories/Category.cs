using CourseSales.Catalog.Api.Features.Courses;
using CourseSales.Catalog.Api.Repositories;

namespace CourseSales.Catalog.Api.Features.Categories
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
