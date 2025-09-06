
using CourseSales.Bus;
using CourseSales.Catalog.Api.Consumers;
using MassTransit;

namespace CourseSales.Catalog.Api
{
    public static class MasstransitConfiguration
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CoursePictureUploadedEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });
                    cfg.ReceiveEndpoint("catalog-microservice.upload-course-picture-uploaded.queue", e =>
                    {
                        e.ConfigureConsumer<CoursePictureUploadedEventConsumer>(context);
                    });
                    // cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
