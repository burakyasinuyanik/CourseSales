using CourseSales.Order.Application.Contracts.Repositories;
using CourseSales.Order.Application.Contracts.UnitOfWork;
using CourseSales.Order.Application.Dto;
using CourseSales.Order.Domain.Entities;
using CourseSales.Shared;
using CourseSales.Shared.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository,
        IGenericRepository<int,Adress> addressRepository,
        IIdentityService identityService,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public  async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if(!request.Items.Any())
                return ServiceResult.Error("Sipariş içerisinde ürün bulunmamaktadır","Sipariş içerisinden en az bir ürün bulunması gerekmektedir.",HttpStatusCode.BadRequest);
        
            var newAdress = new Adress
            {
                District = request.Address.District,
                Line = request.Address.Line,
                Province = request.Address.Province,
                Street = request.Address.Street,
                ZipCode = request.Address.Zipcode,
            };
            addressRepository.Add(newAdress);

            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.discountRate, newAdress.Id);
            foreach (var orderItem in request.Items)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
            }

            order.Adress = newAdress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);

            var paymentId = Guid.Empty;

            order.SetPaidStatus(paymentId);
            orderRepository.Update(order);

           await unitOfWork.CommitAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
