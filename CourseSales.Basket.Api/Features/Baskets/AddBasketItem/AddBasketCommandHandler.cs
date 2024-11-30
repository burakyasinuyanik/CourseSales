using CourseSales.Basket.Api.Data;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using System.Text.Json;

namespace CourseSales.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketCommandHandler(IIdentityService identityService,BasketService basketService ) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            //fats fail


            //TODO: change user ıd
          
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            Data.Basket? currentBasket;
            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.CoursePrice, request.ImageUrl, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);


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

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);





            return ServiceResult.SuccessAsNoContent();

        }

        
    }
}
