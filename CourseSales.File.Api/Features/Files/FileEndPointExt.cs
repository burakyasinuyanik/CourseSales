using Asp.Versioning.Builder;
using CourseSales.File.Api.Features.Files.Delete;
using CourseSales.File.Api.Features.Files.Upload;

namespace CourseSales.File.Api.Features.Files
{
    public static class FileEndPointExt
    {
        public static void AddFileGroupEndPointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .UploadFileGroupItemEndPoint()
                .DeleteFileCommandGroupItemEndPoint()
                .WithTags("files")
                .WithApiVersionSet(apiVersionSet)
                .DisableAntiforgery()
                .RequireAuthorization();
        }
    }
}
