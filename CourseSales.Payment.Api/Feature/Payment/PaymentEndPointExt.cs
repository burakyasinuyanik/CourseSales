using Asp.Versioning.Builder;
using CourseSales.Payment.Api.Feature.Payment.Create;

namespace CourseSales.Payment.Api.Feature.Payment
{
    public static class PaymentEndPointExt
    {
        public static void AddPaymentGroupEntPointExt(this WebApplication app,ApiVersionSet versionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments")
                .WithApiVersionSet(versionSet)
                .WithTags("Payment")
                .CreatePaymentGroupItemEndPoint();
        }
    }
}
