using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.Interfaces;

namespace RentingSystemAPI.Validators
{
    public class CartValidator<T> : AbstractValidator<T> where T : IItem
    {
        public CartValidator()
        {
            RuleFor(c => c.ItemId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(c => c.Quantity)
                .GreaterThan(0);
        }
    }
}