namespace CourseSales.Payment.Api.Feature.Payment.Create
{
    public record class CreatePaymentResponse(bool Status,string? ErrorMessage, Guid? PaymentId);

}
