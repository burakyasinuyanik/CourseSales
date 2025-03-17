using AutoMapper;
using CourseSales.Order.Application.Contracts.Repositories;
using CourseSales.Order.Application.Dto;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders.GetByUserId
{
    public class GetOrderHandler(IOrderRepository orderRepository,
        IIdentityService ıdentityService,
        IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrderResponse>>>
    {
        public async Task<ServiceResult<List<GetOrderResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByUserId(ıdentityService.GetUserId);
            if (orders is null)
                return ServiceResult<List<GetOrderResponse>>.Error("Sipariş bulunmamaktadır", HttpStatusCode.NotFound);

            var response = orders.Select(o => new GetOrderResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();

            return ServiceResult<List<GetOrderResponse>>.SuccessAsOk(response);
        }
    }
}
