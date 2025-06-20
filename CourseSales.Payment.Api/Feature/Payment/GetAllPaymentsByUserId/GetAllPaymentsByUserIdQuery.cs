using CourseSales.Shared;

namespace CourseSales.Payment.Api.Feature.Payment.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery:IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
    
}
