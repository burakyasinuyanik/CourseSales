using CourseSales.Order.Application.Contracts.Refit.PaymentService;
using CourseSales.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Contracts.Refit
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(c =>
            {
                var addresUrlOption = configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();
                c.BaseAddress = new Uri(addresUrlOption!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
            return services;
        }
    }
}
