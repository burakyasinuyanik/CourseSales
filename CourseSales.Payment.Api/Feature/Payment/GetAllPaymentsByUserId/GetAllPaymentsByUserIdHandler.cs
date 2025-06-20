using CourseSales.Payment.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;

namespace CourseSales.Payment.Api.Feature.Payment.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdHandler(AppDbContext appDbContext, IIdentityService identityService) : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;
            var payments = appDbContext.Payments
                .Where(x => x.UserId == userId)
                .Select(x => new GetAllPaymentsByUserIdResponse(
                    x.Id,
                    x.OrderCode,
                    x.Amount.ToString("C"),
                    x.Created,
                    x.Status))
                .ToList();

            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);




        }
    }
}
