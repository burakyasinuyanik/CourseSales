using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CourseSales.File.Api.Features.Files.Upload
{
    public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;

    public class UploadFileCommandHandler(IFileProvider fileProvider)
        : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {
        public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            if(request.File.Length==0)
                return ServiceResult<UploadFileCommandResponse>.Error("Dosya boştur",HttpStatusCode.BadRequest);

            
            var newFileName=$"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
            //var upLoadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath, newFileName);
            var upLoadPath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", newFileName);
            await using var stream = new FileStream(upLoadPath, FileMode.Create);

            await request.File.CopyToAsync(stream,cancellationToken);

            var response =new UploadFileCommandResponse(newFileName,$"files/{newFileName}",request.File.FileName);

            return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response,response.FilePath);

        }
    }

    public record UploadFileCommandResponse(string FileName, string FilePath, string OriginalFileName);

    public static class UploadFileCommandEndPoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) =>
            {

                var result = await mediator.Send(new UploadFileCommand(file));

                return result.ToGenericResult();
            })
                .WithName("Upload")
                .MapToApiVersion(1,0);

            return group;

        }
    }
}
