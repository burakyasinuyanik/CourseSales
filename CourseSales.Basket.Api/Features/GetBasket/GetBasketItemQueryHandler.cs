using CourseSales.Basket.Api.Const;
using CourseSales.Basket.Api.Features.Baskets.Dtos;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace CourseSales.Basket.Api.Features.GetBasket
{
    public class GetBasketItemQueryHandler(IDistributedCache distributedCache,IIdentityService identityService):IRequestHandler<GetBasketItemQuery,ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketItemQuery request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var hasKey = await distributedCache.GetStringAsync(cacheKey,cancellationToken);
            if (string.IsNullOrEmpty(hasKey)) 
             return ServiceResult<BasketDto>.Error("Sepet Bulunamadı", "Sepet Bulunamadı", HttpStatusCode.NotFound);

            var currentBasket = JsonSerializer.Deserialize<BasketDto>(hasKey);

            if(currentBasket!.BasketItems.Count==0)
                return ServiceResult<BasketDto>.Error( "Sepet Boş", HttpStatusCode.NotFound);


            return ServiceResult<BasketDto>.SuccessAsOk(currentBasket);

        }
    }
}
