using CourseSales.Shared.Extensions;
using CourseSales.Shared.Filters;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount
{
    public static class ApplyDiscountCouponEndPoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupEndPoint(this RouteGroupBuilder group
            )
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                {
                    var Result =
                        await mediator.Send(new ApplyDiscountCouponCommand(command.Cuopon, command.DiscountRate));

                    return Result.ToGenericResult();

                })
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();

            return group;
        }
    }
}
