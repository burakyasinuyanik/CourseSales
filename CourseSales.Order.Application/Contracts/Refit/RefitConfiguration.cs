using CourseSales.Order.Application.Contracts.Refit.PaymentService;
using CourseSales.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace CourseSales.Order.Application.Contracts.Refit
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<IdentityOption>()
                .Bind(configuration.GetSection(nameof(IdentityOption)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton<IdentityOption>(sp=>sp.GetRequiredService<IOptions<IdentityOption>>().Value);
            services.AddOptions<ClientSecretOption>()
             .Bind(configuration.GetSection(nameof(ClientSecretOption)))
             .ValidateDataAnnotations()
             .ValidateOnStart();
            services.AddSingleton<ClientSecretOption>(sp => sp.GetRequiredService<IOptions<ClientSecretOption>>().Value);

            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();
            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(c =>
            {
                var addresUrlOption = configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();
                c.BaseAddress = new Uri(addresUrlOption!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
            .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();
            return services;
        }
    }
}
