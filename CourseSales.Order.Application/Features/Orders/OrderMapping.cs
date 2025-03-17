using AutoMapper;
using CourseSales.Order.Application.Dto;
using CourseSales.Order.Application.Features.Orders.GetByUserId;
using CourseSales.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
