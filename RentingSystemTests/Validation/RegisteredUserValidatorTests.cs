using FluentValidation.TestHelper;
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

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.FirstName, null as string);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.FirstName, "name");
        }
        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(user => user.LastName, null as string);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsSpecified()
        {
            _validator.ShouldNotHaveValidationErrorFor(user => user.LastName, "name");
        }
    }
}