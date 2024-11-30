using FluentValidation;

namespace CourseSales.Basket.Api.Features.Baskets.DiscountBasket.ApplyDiscount
{
    public class ApplyDiscountCouponCommandValidator:AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(i => i.DiscountRate).GreaterThan(0).WithMessage("Lütfen indirim 0 dan büyük olmalıdır.");
            RuleFor(i => i.Cuopon).NotEmpty().WithMessage("Lütfen kupon bilgisini giriniz");
        }
    }
}
