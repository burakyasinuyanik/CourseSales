using CourseSales.Catalog.Api.Features.Categories.Dtos;

namespace CourseSales.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
    

    public class GetCategoryByIdQueryHandler(AppDbContext context,IMapper mapper):IRequestHandler<GetCategoryByIdQuery,ServiceResult<CategoryDto>>{
     

        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FindAsync(request.Id,cancellationToken);
            if(hasCategory is null)
                return ServiceResult<CategoryDto>.Error("Categori bulunamadı",$"{request.Id}'li kategori bulunamadı",
            HttpStatusCode.NotFound);

            var categoryAsDto = mapper.Map<CategoryDto>(hasCategory);

            return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);
        }
    }
    public static class GetByIdCategoryGroupEndPoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery(id));

                return result.ToGenericResult();
            });
            return group;
        }
    }

}
