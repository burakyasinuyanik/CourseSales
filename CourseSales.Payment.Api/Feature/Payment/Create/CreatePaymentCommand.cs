using CourseSales.Shared;

namespace CourseSales.Payment.Api.Feature.Payment.Create
{
    public record CreatePaymentCommand(string OrderCode,string CardNumber,string CardHolderName,string CardExpirationDate,string CardSecurityNumber ,decimal Amount):IRequestByServiceResult<Guid>
    {
    }
}
