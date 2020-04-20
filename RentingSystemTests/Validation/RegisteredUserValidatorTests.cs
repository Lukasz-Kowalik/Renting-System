using FluentValidation.TestHelper;
using RentingSystem.Validation;
using RentingSystem.ViewModels.Models;
using Xunit;

namespace RentingSystemTests.Validation
{
    public class RegisteredUserValidatorTest
    {
        private RegisteredUserValidator _validator { get; set; }

        public RegisteredUserValidatorTest()
        {
            _validator = new RegisteredUserValidator();
        }

        private readonly RegisteredUser correctUser = new RegisteredUser
        {
            FirstName = "Jon",
            LastName = "Doe",
            Email = "email@email.com",
            Password = "POI0998#lk",
            ConfirmPassword = "POI0998#lk"
        };

        private readonly RegisteredUser incorrectUser = new RegisteredUser();

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.FirstName, incorrectUser);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.FirstName, correctUser);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.LastName, incorrectUser);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.LastName, correctUser);
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Email, incorrectUser);
        }

        [Fact]
        public void ShouldNotHaveErrorIfEmailIsCorrect()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Email, correctUser);
        }

        [Fact]
        public void ShouldHaveErrorIfEmailDoNotHaveAtSign()
        {
            var user = new RegisteredUser { Email = "SAEFD" };
            _validator.ShouldHaveValidationErrorFor(user => user.Email, user);
        }

        [Fact]
        public void ShouldNotHaveErrorIfPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Password, correctUser);
        }

        [Fact]
        public void ShouldNotHaveErrorIfSecondPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.ConfirmPassword, correctUser);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Password, incorrectUser);
        }

        [Fact]
        public void ShouldHaveErrorIfSecondPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, incorrectUser);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordsAreDifferent()
        {
            var user = new RegisteredUser { Password = "123", ConfirmPassword = "STREW@#$sad21" };
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, user);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordsAreToShort()
        {
            var user = new RegisteredUser { Password = "123", ConfirmPassword = "123" };
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, user);
        }
    }
}