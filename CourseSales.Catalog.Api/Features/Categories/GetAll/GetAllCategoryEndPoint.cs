using AutoMapper;
using CourseSales.Catalog.Api.Features.Categories.Dtos;
using CourseSales.Catalog.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseSales.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryQuery : IRequestByServiceResult<List<CategoryDto>>;

    public class GetAllCategoryHandler(AppDbContext context , IMapper mapper)
        : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken);
            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
        }
    }
    public static class GetAllCategoryEndPoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCategoryQuery());
                return result.ToGenericResult();
            });

            return group;
        }
    }
}
