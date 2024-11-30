using AutoMapper;
using CourseSales.Basket.Api.Features.Baskets.Dtos;
using CourseSales.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace CourseSales.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketItemQueryHandler(IMapper mapper,BasketService basketService) : IRequestHandler<GetBasketItemQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketItemQuery request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
                return ServiceResult<BasketDto>.Error("Sepet Bulunamadı", "Sepet Bulunamadı", HttpStatusCode.NotFound);

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            if (currentBasket!.BasketItems.Count == 0)
                return ServiceResult<BasketDto>.Error("Sepet Boş", HttpStatusCode.NotFound);

            var currentBasketAsBasketDto = mapper.Map<BasketDto>(currentBasket);

            return ServiceResult<BasketDto>.SuccessAsOk(currentBasketAsBasketDto);

        }
    }
}
