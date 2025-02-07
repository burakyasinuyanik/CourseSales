using Asp.Versioning.Builder;

namespace CourseSales.File.Api.Features.Files
{
    public static class FileEndPointExt
    {
        public static void AddFileGroupEndPointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .WithTags("files")
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
