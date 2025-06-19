using FluentValidation;

namespace CourseSales.Payment.Api.Feature.Payment.Create
{
    public class CreatePaymentCommandValidator:AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.OrderCode).NotEmpty().WithMessage("Sipariş numarası zorunludur.");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Kart numarası zorunludur.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Kart sahibi ismi zorunludur.");
            RuleFor(x => x.CardExpirationDate).NotEmpty().WithMessage("Kartın son kullanma tarihi zorunludur.");
            RuleFor(x => x.CardSecurityNumber).NotEmpty().WithMessage("Kartın güvenlik numarası zorunludur.");
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0).WithMessage("Sipariş tutarı 0'dan büyük olmalıdır.");
        }
    }
}
