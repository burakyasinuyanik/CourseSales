using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CourseSales.Shared.Extensions
{
    public static class VersioningExt
    {
        public static IServiceCollection AddVersioningExt(this IServiceCollection services)
        {

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                //versiyon bilgisi yoksa default döner.
                options.AssumeDefaultVersionWhenUnspecified = true;
                //end point rapor bilgisi
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();

                //combine de yapılabilir



            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl=true;
            });

            return services;

        }

        public static ApiVersionSet AddVersionSetExt(this WebApplication app)
        {

            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .HasApiVersion(new ApiVersion(1, 2))

                .HasApiVersion(new ApiVersion(2, 0))
                .ReportApiVersions()
                .Build();

            return apiVersionSet;

        }

    }
}
