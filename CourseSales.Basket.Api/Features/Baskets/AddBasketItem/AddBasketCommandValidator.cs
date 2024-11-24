using FluentValidation;

namespace CourseSales.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketCommandValidator:AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("Kurs ıd boş olamaz");
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("Kurs adı boş olamaz");
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("Kurs fiyatı 0 dan büyük olmalıdır.");
        }
    }
}
