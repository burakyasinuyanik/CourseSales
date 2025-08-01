using Asp.Versioning.Builder;
using CourseSales.Discount.Api.Features.Discounts.Create;
using CourseSales.Discount.Api.Features.Discounts.Delete;
using CourseSales.Discount.Api.Features.Discounts.GetAll;
using CourseSales.Discount.Api.Features.Discounts.GetById;

namespace CourseSales.Discount.Api.Features.Discounts
{
    public static class DiscountEndPointExt
    {

        public static void AddDiscountGroupEndPointExt(this WebApplication app, ApiVersionSet versionSet) {

            app.MapGroup("api/v{version:ApiVersion}/discounts")
                 .CreateDiscountGroupItemEndPoint()
                 .DeleteDiscountByIdGroupItemEndPoint()
                 .GetDiscountByIdGroupItemEndPoint()
                 .GetAllDiscountGroupItemEndPoint()
                 .WithTags("Discount")
                 .WithApiVersionSet(versionSet)
                 .RequireAuthorization();
                

        }
    }
}
