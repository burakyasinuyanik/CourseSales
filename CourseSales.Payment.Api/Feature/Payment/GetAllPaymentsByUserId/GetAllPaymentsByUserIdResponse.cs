using CourseSales.Payment.Api.Repositories;

namespace CourseSales.Payment.Api.Feature.Payment.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(Guid Id,string OrderCode,string Amount,DateTime Created,PaymentStatus Status);
    
}
