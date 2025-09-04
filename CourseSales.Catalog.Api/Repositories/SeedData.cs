using CourseSales.Catalog.Api.Features.Categories;
using CourseSales.Catalog.Api.Features.Courses;

namespace CourseSales.Catalog.Api.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!dbContext.Categories.Any())
                {
                    var categories = new List<Category>
                     {
                         new (){Id=Guid.CreateVersion7(),Name="Development"}
                     };
                    dbContext.Categories.AddRange(categories);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Courses.Any())
                {
                    var category = await dbContext.Categories.FirstAsync();
                    var newRandomUserId = Guid.CreateVersion7();
                    var courses = new List<Course>
                     {
                         new()
                         {
                             Id = Guid.CreateVersion7(),
                             CategoryId = category.Id,
                             Description = "seed data açıklama",
                             ImageUrl = "/seedurl",
                             Name = "SeedCourse",
                             Price = 1,
                             UserId = newRandomUserId,
                             Created = DateTime.UtcNow,
                             Feature = new Feature
                             {
                                 Duration = 1,
                                 EducatorFullName = "Burak Yasin",
                                 Rating = 5
                             }
                         }
                     };
                    dbContext.Courses.AddRange(courses);
                    await dbContext.SaveChangesAsync();
                }


            }
        }
    }
}
