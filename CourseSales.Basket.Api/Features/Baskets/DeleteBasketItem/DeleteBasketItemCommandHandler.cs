using CourseSales.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace CourseSales.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService)
        : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
          
            var hasBasket = await basketService.GetBasketFromCache(cancellationToken);
            if (string.IsNullOrEmpty(hasBasket))
                return ServiceResult.Error("Basket Bulunamadı", "Baskette ürün yok", HttpStatusCode.NotFound);

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(hasBasket);

          var basketItemToDelete=  currentBasket!.BasketItems.FirstOrDefault(i => i.Id == request.CourseId);
            if(basketItemToDelete is null)
                return ServiceResult.Error("Baskette ilgili ürün yok", HttpStatusCode.NotFound);

            currentBasket.BasketItems.Remove(basketItemToDelete);

       

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}

