using CourseSales.Payment.Api.Repositories;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using System.Net;

namespace CourseSales.Payment.Api.Feature.Payment.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService identityService) : IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.CardExpirationDate, request.CardSecurityNumber, request.Amount);

            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Ödeme Başarısız!", errorMessage!, HttpStatusCode.BadRequest);
            }
            var userId =  identityService.GetUserId;
            var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
            newPayment.Status=PaymentStatus.Success;

            await appDbContext.Payments.AddAsync(newPayment);
             appDbContext.SaveChanges();

            return ServiceResult<Guid>.SuccessAsOk(newPayment.Id);
        }




        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {

            await Task.Delay(1000);
            return (true, null);
        }
    }
}
