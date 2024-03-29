﻿using FluentValidation;
using RentingSystemAPI.DTOs.Request;

namespace RentingSystemAPI.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password1)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password2)
            .NotEmpty()
            .NotNull();
            RuleFor(x => x.Email)
              .NotEmpty()
                .NotNull()
                .EmailAddress();
        }
    }
}