using CourseSales.Order.Application.Contracts.Refit.PaymentService;
using CourseSales.Order.Application.Contracts.Repositories;
using CourseSales.Order.Application.Contracts.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.BackgrounServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var service = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var orders = orderRepository.Where(orderRepository => orderRepository.Status == Domain.Entities.OrderStatus.WaitingForPayment).ToList();
            foreach (var order in orders)
            {
                var paymentStatusResponse = await service.GetaymentStatusAsync(order.Code);
                if (paymentStatusResponse.IsPaid)
                {
                    await orderRepository.SetStatus(order.Code, paymentStatusResponse.PaymentId!.Value, Domain.Entities.OrderStatus.Paid);
                    await unitOfWork.CommitAsync();
                }
            }

        }
    }
}
