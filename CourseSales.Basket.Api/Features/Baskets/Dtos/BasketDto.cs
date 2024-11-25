using System.Text.Json.Serialization;

namespace CourseSales.Basket.Api.Features.Baskets.Dtos
{
    public record BasketDto(

        List<BasketItemDto> BasketItems
    )
    {
        [JsonIgnore] public Guid UserId { get; set; }
       
    }

}
