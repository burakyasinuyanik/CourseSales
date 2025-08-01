using Asp.Versioning.Builder;

namespace CourseSales.Order.Api.EndPoints.Orders
{
    public static class OrderEndPointExt
    {
        public static void AddOrderGroupEndPointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/orders")
                .WithTags("Orders")
                .WithApiVersionSet(apiVersionSet)
                .CreateOrderGroupItemPoint()
                .GetOrderGroupItemEndPoint()
                .RequireAuthorization();
        }
    }
}
