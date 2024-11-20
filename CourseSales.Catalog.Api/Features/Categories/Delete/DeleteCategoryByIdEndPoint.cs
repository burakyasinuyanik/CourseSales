using System.Net;

using CourseSales.Catalog.Api.Features.Categories.Dtos;
using CourseSales.Catalog.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;

namespace CourseSales.Catalog.Api.Features.Categories.Delete
{
    public record DeleteCategoryByIdQuery(Guid Id) : IRequestByServiceResult;

    public  class DeleteCategoryByIdQueryHandler(AppDbContext context):IRequestHandler<DeleteCategoryByIdQuery,ServiceResult>
    {
        public async  Task<ServiceResult> Handle(DeleteCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FindAsync(request.Id,cancellationToken);
            if (hasCategory is null)
                return ServiceResult<CategoryDto>.Error("Categori Bulunamadı",$"{request.Id}'li kategori bulunmadı",
                    HttpStatusCode.NotFound);
            context.Categories.Remove(hasCategory);
            await context.SaveChangesAsync(cancellationToken);
            

            return  ServiceResult.SuccessAsNoContent();
        }
    }

    public static class DeleteCategoryByIdEndPoint
    {
        public static RouteGroupBuilder DeleteByIdCategoryGroupItemEndPoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new DeleteCategoryByIdQuery(id));
                return result.ToGenericResult();
            });
            return group;
        }
    }
}
