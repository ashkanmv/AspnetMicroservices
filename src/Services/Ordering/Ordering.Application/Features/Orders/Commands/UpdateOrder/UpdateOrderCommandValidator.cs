using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().WithMessage("{UserName} required")
                .NotNull()
                .GreaterThan(0).WithMessage("ID can not be 0");
            RuleFor(o => o.EmailAddress)
                .NotEmpty().WithMessage("{Email} required");

            RuleFor(o => o.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} required")
                .GreaterThan(0).WithMessage("{TotalPrice} should be more than 0");
        }
    }
}
