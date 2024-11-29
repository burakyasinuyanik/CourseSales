using System.Text.Json.Serialization;

namespace CourseSales.Basket.Api.Features.Baskets.Dtos
{
    public record BasketDto

    
    {
        [JsonIgnore] public Guid UserId { get; init; }
        public List<BasketItemDto> BasketItems { get; init; } = new();
        public BasketDto(Guid userId,List<BasketItemDto> basketItem)
        {
            UserId = userId;
            BasketItems = basketItem;
        }

        public BasketDto()
        {
            
        }
       
    }

}
