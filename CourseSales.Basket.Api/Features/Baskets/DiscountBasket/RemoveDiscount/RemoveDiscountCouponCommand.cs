using System.Net;
using System.Text.Json;
using CourseSales.Basket.Api.Const;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.RemoveDiscount
{
    public record class RemoveDiscountCouponCommand:IRequestByServiceResult
    {
    }
    public class RemoveDiscountCouponCommandHandler(IIdentityService identity,IDistributedCache distributedCache):IRequestHandler<RemoveDiscountCouponCommand,ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var userId = identity.GetUserId;
            var cacheKey = String.Format(BasketConst.BasketCacheKey, userId);
            var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if(string.IsNullOrEmpty(basketAsJson))
                return ServiceResult.Error("Sepet bulunamadı",HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            if(!basket.BasketItems.Any())
                return ServiceResult.Error("Sepette ürün yok", HttpStatusCode.NotFound);
            if(!basket.IsApplyDiscount)
                return ServiceResult.Error("Sepetteki ürünlerde uygulanmış bir indirim bulunmamakta", HttpStatusCode.NotFound);
            basket.ClearDiscount();

            basketAsJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsJson);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponItemEndPoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon", async ( IMediator mediator) =>
            {
                var result = await mediator.Send(new RemoveDiscountCouponCommand());
            }).MapToApiVersion(1, 0);

            return group;
        }
    }

}
