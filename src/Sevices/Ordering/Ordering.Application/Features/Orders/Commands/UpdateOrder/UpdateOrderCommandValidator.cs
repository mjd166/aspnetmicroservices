using FluentValidation;
namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
              .NotEmpty().WithMessage("{userName} is required.")
              .NotNull()
              .MaximumLength(50).WithMessage("{userName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
        }
    }
}
