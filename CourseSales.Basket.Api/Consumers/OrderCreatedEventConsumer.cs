using CourseSales.Basket.Api.Features.Baskets;
using CourseSales.Bus.Events;
using MassTransit;

namespace CourseSales.Basket.Api.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
            await basketService.DeleteBasketCacheAsync(context.Message.UserId);
        }
    }
}
