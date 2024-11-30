using System.Net;
using System.Text.Json;
using CourseSales.Basket.Api.Const;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount
{
    public class ApplyDiscountCouponCommandHandler(IDistributedCache distributedCache, IIdentityService identity) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var userId = identity.GetUserId;
            var cacheKey = String.Format(BasketConst.BasketCacheKey, userId);

            var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
                return ServiceResult.Error("Sepet bulunamadı", HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
            if(!basket.BasketItems.Any())
                return ServiceResult.Error("Sepette indirim yapılacak ürün bulunamadı", HttpStatusCode.NotFound);

            if(basket.IsApplyDiscount)
                return ServiceResult.Error("Daha önce indirim yapılmış", HttpStatusCode.BadRequest);


            basket.ApplyNewDiscount(request.Cuopon, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsJson, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
