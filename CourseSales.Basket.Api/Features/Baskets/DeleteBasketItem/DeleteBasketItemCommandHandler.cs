using System.Net;
using System.Text.Json;
using CourseSales.Basket.Api.Const;
using CourseSales.Basket.Api.Features.Baskets.Dtos;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CourseSales.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identity)
        : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid UserId = identity.GetUserId;
            var hasKey = string.Format(BasketConst.BasketCacheKey, UserId);
            var hasBasket = await distributedCache.GetStringAsync(hasKey, cancellationToken);
            if (string.IsNullOrEmpty(hasBasket))
                return ServiceResult.Error("Basket Bulunamadı", "Baskette ürün yok", HttpStatusCode.NotFound);

            var currentBasket = JsonSerializer.Deserialize<BasketDto>(hasBasket);

          var basketItemToDelete=  currentBasket!.BasketItems.FirstOrDefault(i => i.Id == request.CourseId);
            if(basketItemToDelete is null)
                return ServiceResult.Error("Baskette ilgili ürün yok", HttpStatusCode.NotFound);

            currentBasket.BasketItems.Remove(basketItemToDelete);

            var currentBasketAsString = JsonSerializer.Serialize(currentBasket);

            await distributedCache.SetStringAsync(hasKey,currentBasketAsString, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}

