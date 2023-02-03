using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    internal class CheckoutOrderRequestValidation : AbstractValidator<CheckoutOrderRequest>
    {
        public CheckoutOrderRequestValidation()
        {
            RuleFor(o => o.UserName).NotEmpty().WithMessage("{UserName} required")
                .NotNull()
                .MaximumLength(50).WithMessage("cannot be more than 50 character");
            RuleFor(o => o.EmailAddress)
                .NotEmpty().WithMessage("{Email} required");

            RuleFor(o => o.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} required")
                .GreaterThan(0).WithMessage("{TotalPrice} should be more than 0");
        }
    }
}
