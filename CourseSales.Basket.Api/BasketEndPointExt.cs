using Asp.Versioning.Builder;
using CourseSales.Basket.Api.Features.Baskets.AddBasketItem;

namespace CourseSales.Basket.Api
{
    public static class BasketEndPointExt
    {
        public static void  AddBasketGroupEndPointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketGroupItemEndPoint();

        }
    }
}
