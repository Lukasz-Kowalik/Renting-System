using FluentValidation.TestHelper;
using RentingSystem.Models.Accounts;
using RentingSystem.Validation;
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

        private RegisteredUser corectUser = new RegisteredUser
        {
            FirstName = "Jon",
            LastName = "Doe",
            Email = "email@email.com",
            Password = "POI0998#lk",
            ConfirmPassword = "POI0998#lk"
        };

        private RegisteredUser incorecUser = new RegisteredUser();

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.FirstName, incorecUser);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.FirstName, corectUser);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.LastName, incorecUser);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.LastName, corectUser);
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Email, incorecUser);
        }

        [Fact]
        public void ShouldNotHaveErrorIfEmailIsCorrect()
        {
            _validator.ShouldNotHaveValidationErrorFor(user=>user.Email,corectUser);
        }
        [Fact]
        public void ShouldHaveErrorIfEmailDoNotHaveAtSign()
        {
            var user = new RegisteredUser {Email = "SAEFD"};
            _validator.ShouldHaveValidationErrorFor(user => user.Email, user);
        }

        [Fact]
        public void ShouldNotHaveErrorIfPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user=>user.Password,corectUser);
        }
         [Fact]
        public void ShouldNotHaveErrorIfSecondPasswordIsNotNull()
        {
            _validator.ShouldNotHaveValidationErrorFor(user=>user.ConfirmPassword,corectUser);
        }
        [Fact]
        public void ShouldHaveErrorIfPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.Password, incorecUser);
        }
        [Fact]
        public void ShouldHaveErrorIfSecondPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.ConfirmPassword, incorecUser);
        }
        [Fact]
        public void ShouldHaveErrorIfPasswordsAreDifferent()
        {
            var user = new RegisteredUser {Password = "123", ConfirmPassword = "STREW@#$sad21"};
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