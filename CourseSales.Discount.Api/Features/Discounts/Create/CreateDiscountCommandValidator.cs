using FluentValidation;

namespace CourseSales.Discount.Api.Features.Discounts.Create
{
    public class CreateDiscountCommandValidator:AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Kod girilmesi zorunludur")
                .Length(10).WithMessage("Kod 10 karakterli olması gerekmektedir.");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("İndirim oranının girilmesi zorunludur");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı girilmesi zorunludur");
            RuleFor(x => x.Expired).NotEmpty().WithMessage("Son kullanım tarihinin girilmesi zorunludur");
        }
    }
}
