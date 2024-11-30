using System.Text.Json;
using CourseSales.Basket.Api.Const;
using CourseSales.Basket.Api.Features.Baskets.Dtos;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using CourseSales.Basket.Api.Data;

namespace CourseSales.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketCommandHandler(IDistributedCache distributedCache,IIdentityService identityService ) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            //fats fail


            //TODO: change user ıd
            var userId = identityService.GetUserId;
            //basket:userId
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            Data.Basket? currentBasket;
            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(userId, [newBasketItem]);
                await CreateCacheAsyn(currentBasket, cacheKey, cancellationToken);


                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            var existingBasketItem = currentBasket!.BasketItems.FirstOrDefault(c => c.Id == request.CourseId);
            if (existingBasketItem is not null)
            {
                currentBasket.BasketItems.Remove(existingBasketItem);

            }

            currentBasket.BasketItems.Add(newBasketItem);


            currentBasket.ApplyAvaibleDiscount();

            await CreateCacheAsyn(currentBasket, cacheKey, cancellationToken);




            return ServiceResult.SuccessAsNoContent();

        }

        private async Task CreateCacheAsyn(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

        }
    }
}
