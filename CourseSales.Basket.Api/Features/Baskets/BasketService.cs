using System.Text.Json;
using CourseSales.Basket.Api.Const;
using CourseSales.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace CourseSales.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identity, IDistributedCache distributedCache)
    {
        private string GetCacheKey() => String.Format(BasketConst.BasketCacheKey, identity.GetUserId);
        private string GetCacheKey(Guid userId) => String.Format(BasketConst.BasketCacheKey,userId);
        public async Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            var cacheKey = GetCacheKey();
            return await distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
        }
        public async Task DeleteBasketCacheAsync(Guid userId)
        {
            await distributedCache.RemoveAsync(GetCacheKey(userId));

        }
    }
}
