using CourseSales.Shared;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount
{
    public record ApplyDiscountCouponCommand(string Cuopon, float DiscountRate) : IRequestByServiceResult;

}
