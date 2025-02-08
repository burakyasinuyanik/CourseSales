using Asp.Versioning.Builder;
using CourseSales.File.Api.Features.Files.Upload;

namespace CourseSales.File.Api.Features.Files
{
    public static class FileEndPointExt
    {
        public static void AddFileGroupEndPointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .UploadFileGroupItenEndPoint()
                .WithTags("files")
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
