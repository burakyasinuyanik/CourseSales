using CourseSales.Discount.Api.Features.Discounts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace CourseSales.Discount.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Features.Discounts.Discount> Discounts { get; set; }

        public static AppDbContext Create(IMongoDatabase mongoDatabase)
        {
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(mongoDatabase.Client, mongoDatabase.DatabaseNamespace.DatabaseName);
            return  new AppDbContext(optionBuilder.Options);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
