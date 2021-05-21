using FluentValidation;
using RentingSystemAPI.DTOs.Request;

namespace RentingSystemAPI.Validators
{
    public class ItemValidator : AbstractValidator<ItemRequest>
    {
        public ItemValidator()
        {
            RuleFor(x => x.MaxQuantity)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}