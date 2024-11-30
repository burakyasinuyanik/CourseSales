using CourseSales.Shared.Extensions;
using CourseSales.Shared.Filters;
using MediatR;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount
{
    public static class ApplyDiscountCouponEndPoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupEndPoint(this RouteGroupBuilder group
            )
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(new ApplyDiscountCouponCommand(command.Cuopon, command.DiscountRate));

                    return result.ToGenericResult();

                })
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();

            return group;
        }
    }
}
