using System.Text.Json.Serialization;

namespace CourseSales.Basket.Api.Features.Baskets.Dtos
{
    public record BasketDto


    {
        [JsonIgnore] public Guid UserId { get; init; }
        public List<BasketItemDto> BasketItems { get; init; } = new();
        public float? DiscountRate { get; init; }
        public string? Coupon { get; init; }

        public bool IsApplyDiscount { get; init; }
        public decimal TotalPrice => BasketItems.Sum(item => item.CoursePrice);

        public decimal? TotalPriceAppliedDiscount => !IsApplyDiscount ? null : BasketItems.Sum(i => i.PriceByApplyDiscountRate);
        //{
        //    get
        //    {
        //        if (!IsApplyDiscount)
        //        {
        //            return null;
        //        }

        //        return BasketItems.Sum(x => x.PriceByApplyDiscountRate);

        //    }
        //}
        public BasketDto(Guid userId, List<BasketItemDto> basketItem)
        {
            UserId = userId;
            BasketItems = basketItem;
        }

        public BasketDto()
        {

        }

    }

}
