using System.Text.Json;
using CourseSales.Basket.Api.Const;
using CourseSales.Basket.Api.Features.Baskets.Dtos;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

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

            BasketDto? currendBasket;
            var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currendBasket = new BasketDto(userId, [newBasketItem]);
                await CreateCacheAsyn(currendBasket, cacheKey, cancellationToken);


                return ServiceResult.SuccessAsNoContent();
            }

            currendBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
            var existingBasketItem = currendBasket!.BasketItems.FirstOrDefault(c => c.Id == request.CourseId);
            if (existingBasketItem is not null)
            {
                currendBasket.BasketItems.Remove(existingBasketItem);

            }

            currendBasket.BasketItems.Add(newBasketItem);




            await CreateCacheAsyn(currendBasket, cacheKey, cancellationToken);




            return ServiceResult.SuccessAsNoContent();

        }

        private async Task CreateCacheAsyn(BasketDto basketDto, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basketDto);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);

        }
    }
}
