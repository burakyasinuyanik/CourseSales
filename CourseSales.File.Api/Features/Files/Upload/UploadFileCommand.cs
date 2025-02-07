using CourseSales.Shared;
using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Security.Cryptography.X509Certificates;

namespace CourseSales.File.Api.Features.Files.Upload
{
    public record UploadFileCommand(IFormFile file) : IRequestByServiceResult<UploadFileCommandResponse>;

    public class UploadFileCommandHandler(IFileProvider fileProvider)
        : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {
        public Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public record UploadFileCommandResponse(string FileName, string FilePath, string OriginalFileName);

    public static class UploadFileCommandEndPoint
    {
        public static RouteGroupBuilder UploadFileGroupItenEndPoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (UploadFileCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
            })
                .WithName("Upload")
                .MapToApiVersion(1,0);

            return group;

        }
    }
}
