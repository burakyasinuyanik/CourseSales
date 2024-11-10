using FluentValidation;

namespace CourseSales.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Categori ismi boş olamaz.")
                .MaximumLength(100).WithMessage("{PropertyName} Maksimum uzunluk 100 karakter olabilir.")
                .MinimumLength(3).WithMessage("{PropertyName} Minimum 3 karakter giriniz.");

        }
    }
}
