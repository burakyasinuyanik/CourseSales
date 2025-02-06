using Asp.Versioning.Builder;
using CourseSales.Discount.Api.Features.Discounts.Create;
using CourseSales.Discount.Api.Features.Discounts.Delete;

namespace CourseSales.Discount.Api.Features.Discounts
{
    public static class DiscountEndPointExt
    {

        public static void AddDiscountGroupEndPointExt(this WebApplication app, ApiVersionSet versionSet) {

            app.MapGroup("api/v{version:ApiVersion}/discounts")
                 .CreateDiscountGroupItemEndPoint()
                 .DeleteDiscountByIdGroupItemEndPoint()
                 .WithTags("Discount")
                 .WithApiVersionSet(versionSet);
                

        }
    }
}
