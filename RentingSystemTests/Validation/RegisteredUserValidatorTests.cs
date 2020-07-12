using FluentValidation.TestHelper;
using RentingSystem.Validation;
using RentingSystem.ViewModels.DTOs;
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

        private readonly UserDto _correctUserDto = new UserDto
        {
            FirstName = "Jon",
            LastName = "Doe",
            Email = "email@email.com",
            Password = "POI0998#lk",
            ConfirmPassword = "POI0998#lk"
        };

        private readonly UserDto _incorrectUserDto = new UserDto();

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.FirstName, _incorrectUserDto);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.FirstName, _correctUserDto);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.LastName, _incorrectUserDto);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.LastName, _correctUserDto);
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Email, _incorrectUserDto);
        }

        [Fact]
        public void ShouldNotHaveErrorIfEmailIsCorrect()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Email, _correctUserDto);
        }

        [Fact]
        public void ShouldHaveErrorIfEmailDoNotHaveAtSign()
        {
            var user = new UserDto { Email = "SAEFD" };
            _validator.ShouldHaveValidationErrorFor(user => user.Email, user);
        }

        [Fact]
        public void ShouldNotHaveErrorIfPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.Password, _correctUserDto);
        }

        [Fact]
        public void ShouldNotHaveErrorIfSecondPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.ConfirmPassword, _correctUserDto);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Password, _incorrectUserDto);
        }

        [Fact]
        public void ShouldHaveErrorIfSecondPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, _incorrectUserDto);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordsAreDifferent()
        {
            var user = new UserDto { Password = "123", ConfirmPassword = "STREW@#$sad21" };
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, user);
        }

        [Fact]
        public void ShouldHaveErrorIfPasswordsAreToShort()
        {
            var user = new UserDto { Password = "123", ConfirmPassword = "123" };
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, user);
        }
    }
}