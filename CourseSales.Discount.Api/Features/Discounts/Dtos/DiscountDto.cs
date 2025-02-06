namespace CourseSales.Discount.Api.Features.Discounts.Dtos
{
    public record class DiscountDto(
        Guid Id,
        string Code,
        float Rate,
        Guid UserId,
        DateTime Expired
        );
    
}
