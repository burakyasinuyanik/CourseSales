namespace CourseSales.Basket.Api.Features.Baskets.Dtos
{
    public record BasketItemDto(
        Guid Id,
       
        string Name,
        decimal CoursePrice,
        string ImageUrl,
        decimal? PriceByApplyDiscountRate
    );

}
