using Asp.Versioning.Builder;
using CourseSales.Basket.Api.Features.Baskets.AddBasketItem;
using CourseSales.Basket.Api.Features.Baskets.DeleteBasketItem;
using CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount;
using CourseSales.Basket.Api.Features.Baskets.DiscountBasket.RemoveDiscount;
using CourseSales.Basket.Api.Features.Baskets.GetBasket;

namespace CourseSales.Basket.Api.Features.Baskets
{
    public static class BasketEndPointExt
    {
        public static void AddBasketGroupEndPointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketGroupItemEndPoint()
                .DeleteBasketItemGroupEndPoint()
                .GetBasketItemGroupItem()
                .ApplyDiscountCouponGroupEndPoint()
                .RemoveDiscountCouponGroupItemEndPoint()
                .RequireAuthorization();

        }
    }
}
