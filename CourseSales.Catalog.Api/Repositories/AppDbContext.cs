using CourseSales.Catalog.Api.Features.Categories;
using CourseSales.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace CourseSales.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

          

            
        }
    }
}
