﻿using AutoMapper;
using CourseSales.Basket.Api.Data;
using CourseSales.Basket.Api.Features.Baskets.AddBasketItem;
using CourseSales.Basket.Api.Features.Baskets.Dtos;

namespace CourseSales.Basket.Api.Features.Baskets
{
    public class BasketMapping:Profile
    {
        public BasketMapping()
        {
            CreateMap<AddBasketItemCommand, BasketDto>().ReverseMap();
            CreateMap<Data.Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
