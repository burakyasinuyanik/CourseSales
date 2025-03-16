using CourseSales.Order.Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.discountRate)
                .GreaterThan(0).When(x => x.discountRate.HasValue)
                .WithMessage("{PropertyName} 0'dan büyük ve pozitif sayı olmalıdır");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("{PropertyName} zorunludur")
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("{PropertyName} Siparişin itemları olmalıdır");

            RuleForEach(x => x.Items)
                .SetValidator(new OrderItemDtoValidator());


            RuleFor(x => x.Payment).NotNull().WithMessage("{PropertyName} zorunludur")
                .SetValidator(new PaymentDtoValidator());
        }
    }

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Line)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.Zipcode)
                .NotEmpty().WithMessage("{PropertyName} zorunludur")
                .Matches(@"^\d{5}$").WithMessage("{PropertyName} 5 uzunluklu numaradan oluşmalıdır");
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("{PropertyName} zorunludur");
        }
    }

    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.Cvc)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.Expiration)
                .NotEmpty().WithMessage("{PropertyName} zorunludur");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} 0 dan büyük olmalıdır");
        }
    }
}
