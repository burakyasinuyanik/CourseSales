using MassTransit;

namespace CourseSales.Discount.Api.Repositories
{
    public static class SeedData
    {
        public static async Task  AddSeedDataExt(this WebApplication application)
        {

            using var scope=application.Services.CreateAsyncScope();
            var context=   scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!context.Discounts.Any()) {

             await   context.Discounts.AddAsync(new Features.Discounts.Discount()
                {
                    Code = "test code",
                    Created = DateTime.Now,
                    Expired = DateTime.Now.AddDays(1),
                    Rate = (float)1.2 ,
                    Id=NewId.NextSequentialGuid(),
                    UserId=NewId.NextSequentialGuid(),
                });

               await context.SaveChangesAsync();
            }
           
        }
    }
}
