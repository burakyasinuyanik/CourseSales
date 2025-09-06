
using CourseSales.Basket.Api.Consumers;
using CourseSales.Bus;
using CourseSales.Bus.Events;
using MassTransit;

namespace CourseSales.Basket.Api
{
    public static class MasstransitConfiguration
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });
                    cfg.ReceiveEndpoint("basket-microservice.order-created.queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
                    });
                    // cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
