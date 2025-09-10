using CourseSales.Payment.Api.Repositories;
using CourseSales.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseSales.Payment.Api.Feature.Payment.GetStatus
{
    public record GetPaymentStatusQuery(string OrderCode):IRequestByServiceResult<GetPaymentStatusResponse>;
    public record GetPaymentStatusResponse(bool IsPaid,Guid PaymentId);

    public class GetPaymentStatusQueryHandler(AppDbContext context) : IRequestHandler<GetPaymentStatusQuery, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.OrderCode == request.OrderCode,cancellationToken);
            if (payment is null)
            {
                return  ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(false,Guid.Empty));
            }
            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(payment.Status == PaymentStatus.Success,payment.Id));
        }
    }
}
