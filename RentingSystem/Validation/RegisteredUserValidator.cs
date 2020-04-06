using FluentValidation;
using RentingSystem.Models.Accounts;
using System.Text.RegularExpressions;

namespace RentingSystem.Validation
{
    public class RegisteredUserValidator : AbstractValidator<RegisteredUser>
    {
        private const string PasswordRequiredMessage = "Password is required.";
        private const string PasswordsDoNotMatchMessage = "Passwords do not match.";
        private const string PasswordLengthMessage = "Password should be have at least 8 character length.";
        private const string PasswordRequirementMessage = @"Select stronger password.(letters,numbers and symbols)";

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
                .Length(8)
                .WithMessage(PasswordLengthMessage);

            RuleFor(user => user.ConfirmPassword)
                .NotNull()
                .WithMessage(PasswordRequiredMessage)
                .Equal(user => user.Password)
                .WithMessage(PasswordsDoNotMatchMessage)
                .Must(IsPasswordIsStrong)
                .WithMessage(PasswordRequirementMessage)
                .Length(8)
                .WithMessage(PasswordLengthMessage);
        }

        private bool IsPasswordIsStrong(string password)
        {
            var pattern = "^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))";
            var regex = new Regex(pattern);
            return password != null && regex.IsMatch(password);
        }
    }
}