﻿using FluentValidation;
using RentingSystem.ViewModels.DTOs;
using System.Text.RegularExpressions;

namespace RentingSystem.Validation
{
    public sealed class RegisteredUserValidator : AbstractValidator<UserDto>
    {
        private const string PasswordRequiredMessage = "Password is required.";
        private const string PasswordsDoNotMatchMessage = "Passwords do not match.";
        private const string PasswordLengthMessage = "Password should be have at least 8 character length.";
        private const string PasswordRequirementMessage = "Select stronger password.(letters(upper and lower case),numbers and symbols)";

        public RegisteredUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotNull()
                .WithMessage("First name is required");
            RuleFor(user => user.LastName)
                .NotNull()
                .WithMessage("Last name is required");

            RuleFor(user => user.Email)
                .NotNull()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Incorrect email");

            RuleFor(user => user.Password)
                .NotNull()
                .WithMessage(PasswordRequiredMessage)
                .Equal(user => user.ConfirmPassword)
                .WithMessage(PasswordsDoNotMatchMessage)
                .Must(IsPasswordIsStrong)
                .WithMessage(PasswordRequirementMessage)
                .MinimumLength(8)
                .WithMessage(PasswordLengthMessage);

            RuleFor(user => user.ConfirmPassword)
                .NotNull()
                .WithMessage(PasswordRequiredMessage)
                .Equal(user => user.Password)
                .WithMessage(PasswordsDoNotMatchMessage)
                .Must(IsPasswordIsStrong)
                .WithMessage(PasswordRequirementMessage)
                .MinimumLength(8)
                .WithMessage(PasswordLengthMessage);
        }

        private static bool IsPasswordIsStrong(string password)
        {
            const string pattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            var regex = new Regex(pattern);
            return password != null && regex.IsMatch(password);
        }
    }
}