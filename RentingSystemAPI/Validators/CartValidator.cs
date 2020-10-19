using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RentingSystemAPI.DTOs.Request;

namespace RentingSystemAPI.Validators
{
    public class CartValidator : AbstractValidator<AddItemRequest>
    {
        public CartValidator()
        {
            RuleFor(c => c.ItemId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.Quantity)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}