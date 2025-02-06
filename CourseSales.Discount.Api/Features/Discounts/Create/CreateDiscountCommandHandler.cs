using AutoMapper;
using CourseSales.Discount.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System.Net;

namespace CourseSales.Discount.Api.Features.Discounts.Create
{
    public class CreateDiscountCommandHandler(AppDbContext context, IMapper mapper, IIdentityService identityService) :
        IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountExits =  context.Discounts.Any(x=>x.Code==request.Code);
            if (discountExits)
                return ServiceResult.Error("Kod Mevcut", HttpStatusCode.BadRequest);

            var newDiscount = mapper.Map<Discount>(request);
            newDiscount.UserId = identityService.GetUserId;
            newDiscount.Created= DateTime.Now;
            await context.AddAsync(newDiscount, cancellationToken);
            context.SaveChanges();
            return ServiceResult.SuccessAsNoContent();
        }
    }
}


