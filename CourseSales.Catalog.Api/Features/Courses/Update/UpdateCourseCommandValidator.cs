namespace CourseSales.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandValidator:AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim boş olamaz")
                .MaximumLength(100).WithMessage("Maksimum 100 karakter girebilirsiniz");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz")
                .MaximumLength(100).WithMessage("Maksimum 1000 karakter girebilirsiniz");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat boş olamaz");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori ıd boş olamaz");
        }
    }
}
