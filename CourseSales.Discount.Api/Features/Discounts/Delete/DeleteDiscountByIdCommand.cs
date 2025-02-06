using CourseSales.Discount.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Extensions;
using MediatR;

namespace CourseSales.Discount.Api.Features.Discounts.Delete
{
    public record DeleteDiscountByIdCommand(Guid Id):IRequestByServiceResult;


    public class DeleteDiscountByIdHandler(AppDbContext context) : IRequestHandler<DeleteDiscountByIdCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteDiscountByIdCommand request, CancellationToken cancellationToken)
        {
           var hasCode=await context.Discounts.FindAsync(new object[] {request.Id});
            if(hasCode is  null)
                return ServiceResult.Error("Silmek istediğiniz kod bulunamadı.",System.Net.HttpStatusCode.NotFound);

            context.Discounts.Remove(hasCode);
            context.SaveChanges();

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class DeleteDiscountByIdGroupEndPoint
    {
        public static RouteGroupBuilder DeleteDiscountByIdGroupItemEndPoint(this RouteGroupBuilder group) {

            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new DeleteDiscountByIdCommand(id));
                return result.ToGenericResult();
            })
                .MapToApiVersion(1.0)
                .WithName("DeleteDiscount");

            return group;
        
        }
    }
}
