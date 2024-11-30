namespace CourseSales.Basket.Api.Data
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal CoursePrice { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? PriceByApplyDiscountRate { get; set; }

        public BasketItem(Guid id, string name, decimal coursePrice, string? imageUrl, decimal? priceByApplyDiscountRate)
        {
            Id = id;
            Name = name;
            CoursePrice = coursePrice;
            ImageUrl = imageUrl;
            PriceByApplyDiscountRate = priceByApplyDiscountRate;
        }
    }
}
