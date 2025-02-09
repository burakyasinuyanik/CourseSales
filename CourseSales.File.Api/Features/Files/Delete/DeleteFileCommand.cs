using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Net;

namespace CourseSales.File.Api.Features.Files.Delete
{
    public record DeleteFileCommand(string FileName):IRequestByServiceResult;

    public class DeleteFileCommandHandler(IFileProvider provider) : IRequestHandler<DeleteFileCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileInfo= provider.GetFileInfo(Path.Combine("files",request.FileName));
            if (!fileInfo.Exists)
                return await Task.FromResult(ServiceResult.ErrorNotFound());

            System.IO.File.Delete(fileInfo.PhysicalPath!);

            return await Task.FromResult(ServiceResult.SuccessAsNoContent());
        }
    }

    public static class DeleteFileCommandEndPoint
    {
        public static RouteGroupBuilder DeleteFileCommandGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/", async ([FromBody] DeleteFileCommand request, IMediator mediator) =>
            {

                var result= await mediator.Send(request);

                return result.ToGenericResult();
            })
                .WithName("delete")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
