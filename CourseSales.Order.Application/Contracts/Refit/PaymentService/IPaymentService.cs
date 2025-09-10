using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Contracts.Refit.PaymentService
{
    public interface IPaymentService
    {
        [Post("/api/v1/payments")]
        Task<CreatePaymentResponse> CreatePaymentAsync([Body] CreatePaymentRequest paymentRequest);
        [Get("/api/v1/payments/status/{orderCode}")]
        Task<GetPaymentStatusResponse> GetaymentStatusAsync(string orderCode);
    }
}
