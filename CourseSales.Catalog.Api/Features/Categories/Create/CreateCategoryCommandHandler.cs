using CourseSales.Catalog.Api.Repositories;
using CourseSales.Shared;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CourseSales.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (existCategory)
            {
                ServiceResult<CreateCategoryResponse>.Error("Category Mevcut", $"{request.Name} daha önce mevcut olan kategori ismi",
                    HttpStatusCode.BadRequest
                    );
            }

            var category = new Category { Name = request.Name, Id = NewId.NextSequentialGuid() };

            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"<empty>");
        }
    }
}
