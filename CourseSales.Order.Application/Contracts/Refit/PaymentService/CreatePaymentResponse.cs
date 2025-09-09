namespace CourseSales.Order.Application.Contracts.Refit.PaymentService
{
    public record class CreatePaymentResponse(bool Status, string? ErrorMessage, Guid? PaymentId);

}
