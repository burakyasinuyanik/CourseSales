using CourseSales.Web.Options;
using Microsoft.Extensions.Options;

namespace CourseSales.Web.Extensions
{
    public static class OptionsExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
           services.AddOptions< IdentityOption >().BindConfiguration(nameof(IdentityOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton<IdentityOption>(sp=>sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            services.AddOptions<GatewayOption>().BindConfiguration(nameof(GatewayOption))
              .ValidateDataAnnotations()
              .ValidateOnStart();
            services.AddSingleton<GatewayOption>(sp => sp.GetRequiredService<IOptions<GatewayOption>>().Value);


            services.AddOptions<MicroServiceOption>().BindConfiguration(nameof(MicroServiceOption))
              .ValidateDataAnnotations()
              .ValidateOnStart();
            services.AddSingleton<MicroServiceOption>(sp => sp.GetRequiredService<IOptions<MicroServiceOption>>().Value);
            return services;
        }
    }
}
