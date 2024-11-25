using FluentValidation;

namespace CourseSales.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator:AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(i=>i.CourseId).NotEmpty().WithMessage("Geçerli kurs Id giriniz.");
        }
    }
}
