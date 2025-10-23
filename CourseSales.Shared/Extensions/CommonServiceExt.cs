using CourseSales.Shared.GlobalExceptionHandlers;
using CourseSales.Shared.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseSales.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddAutoMapper(assembly);
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }
    }
}
