using FluentValidation;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.MaxQuantity)
                .NotEmpty()
                .GreaterThan(0)
                .GreaterThanOrEqualTo(x => x.Quantity);

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(x => x.MaxQuantity);
        }
    }
}