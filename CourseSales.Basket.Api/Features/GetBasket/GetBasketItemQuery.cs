using CourseSales.Basket.Api.Features.Baskets.Dtos;
using CourseSales.Shared;

namespace CourseSales.Basket.Api.Features.GetBasket
{
    public record GetBasketItemQuery : IRequestByServiceResult<BasketDto>;

}
