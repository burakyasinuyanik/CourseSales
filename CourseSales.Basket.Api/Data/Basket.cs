using CourseSales.Basket.Api.Features.Baskets.Dtos;
using System.Text.Json.Serialization;

namespace CourseSales.Basket.Api.Data
{
    //anamic model=rich domain model (behavior(yardımcı metot) +data)
    public class Basket
    {

        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);
        public decimal TotalPrice => BasketItems.Sum(item => item.CoursePrice);

        public Basket(Guid userId, List<BasketItem> basketItems)
        {
            UserId = userId;
            BasketItems = basketItems;
        }

        public decimal? TotalPriceAppliedDiscount
        {
            get
            {
                if (!IsApplyDiscount)
                {
                    return null;
                }

                return BasketItems.Sum(x => x.PriceByApplyDiscountRate);

            }
        }

        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;

            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscountRate = item.CoursePrice * (decimal)(1 - discountRate);

            }

        }

        public void ApplyAvaibleDiscount()
        {
            if (!IsApplyDiscount)
            { return; }

            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscountRate = item.CoursePrice * (decimal)(1 - DiscountRate!);

            }

        }

        public void ClearDiscount()
        {
            Coupon = null;
            DiscountRate = null;

            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscountRate = null;

            }

        }
    }
}
