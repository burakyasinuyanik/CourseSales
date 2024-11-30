using CourseSales.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {


            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
                return ServiceResult.Error("Sepet bulunamadı", HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;
            if (!basket.BasketItems.Any())
                return ServiceResult.Error("Sepette indirim yapılacak ürün bulunamadı", HttpStatusCode.NotFound);

            if (basket.IsApplyDiscount)
                return ServiceResult.Error("Daha önce indirim yapılmış", HttpStatusCode.BadRequest);


            basket.ApplyNewDiscount(request.Cuopon, request.DiscountRate);

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
