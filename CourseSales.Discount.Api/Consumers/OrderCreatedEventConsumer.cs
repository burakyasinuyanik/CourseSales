using CourseSales.Bus.Events;
using CourseSales.Discount.Api.Features.Discounts;
using CourseSales.Discount.Api.Repositories;
using MassTransit;

namespace CourseSales.Discount.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var discount = new Features.Discounts.Discount
            {
                Id = Guid.CreateVersion7(),
                Code = DiscountCodeGenarator.Generate(10),
                Rate = 0.1f,
                Expired = DateTime.Now.AddMonths(1),
                UserId = context.Message.UserId,
                Created = DateTime.Now,

            };
           await dbContext.Discounts.AddAsync(discount);
          await  dbContext.SaveChangesAsync();
        }
    }
}