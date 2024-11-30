using CourseSales.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.RemoveDiscount
{
    public record  RemoveDiscountCouponCommand : IRequestByServiceResult
    {
    }
    public class RemoveDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {

            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
                return ServiceResult.Error("Sepet bulunamadı", HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            if (!basket.BasketItems.Any())
                return ServiceResult.Error("Sepette ürün yok", HttpStatusCode.NotFound);
            if (!basket.IsApplyDiscount)
                return ServiceResult.Error("Sepetteki ürünlerde uygulanmış bir indirim bulunmamakta", HttpStatusCode.NotFound);
            basket.ClearDiscount();


            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponItemEndPoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new RemoveDiscountCouponCommand());
            }).MapToApiVersion(1, 0);

            return group;
        }
    }

}
