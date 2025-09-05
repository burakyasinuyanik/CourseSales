using CourseSales.File.Api.Consumers;
using MassTransit;

namespace CourseSales.Bus
{
    public static class MasstransitConfiguration
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UploadCoursePictureCommandConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), h =>
                    {
                        h.Username(busOptions.UserName);
                        h.Password(busOptions.Password);
                    });
                    cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue", e =>
                    {
                        e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(context);
                    });
                    // cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
