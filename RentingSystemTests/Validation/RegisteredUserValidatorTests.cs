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

        private readonly UserVm _correctUserVm = new UserVm
        {
            FirstName = "Jon",
            LastName = "Doe",
            Email = "email@email.com",
            Password = "POI0998#lk",
            ConfirmPassword = "POI0998#lk"
        };

        private readonly UserVm _incorrectUserVm = new UserVm();

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.FirstName, _incorrectUserVm);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.FirstName, _correctUserVm);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.LastName, _incorrectUserVm);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.LastName, _correctUserVm);
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Email, _incorrectUserVm);
        }

        [Fact]
        public void ShouldNotHaveErrorIfEmailIsCorrect()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Email, _correctUserVm);
        }

        [Fact]
        public void ShouldHaveErrorIfEmailDoNotHaveAtSign()
        {
            var user = new UserVm { Email = "SAEFD" };
            _validator.ShouldHaveValidationErrorFor(user => user.Email, user);
        }

        [Fact]
        public void ShouldNotHaveErrorIfPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Password, _correctUserVm);
        }

        [Fact]
        public void ShouldNotHaveErrorIfSecondPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.ConfirmPassword, _correctUserVm);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Password, _incorrectUserVm);
        }

        [Fact]
        public void ShouldHaveErrorIfSecondPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, _incorrectUserVm);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordsAreDifferent()
        {
            var user = new UserVm { Password = "123", ConfirmPassword = "STREW@#$sad21" };
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, user);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordsAreToShort()
        {
            var user = new UserVm { Password = "123", ConfirmPassword = "123" };
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, user);
        }
    }
}