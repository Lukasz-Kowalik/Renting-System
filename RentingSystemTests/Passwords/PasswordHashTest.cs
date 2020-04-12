using RentingSystem.Authorization;
using RentingSystem.Validation;
using Xunit;

namespace RentingSystemTests.Passwords
{
    public class PasswordHashTest
    {
        [Theory]
        [InlineData("password", "password", true)]
        [InlineData("password", "password1", false)]
        public void Encrypt_PasswordIsEncryptedProperly_CheckIfPasswordsAreThisSame(string pas, string pas2, bool expectedResult)
        {
            var passwordHash = new PasswordHash(pas);
            var result = passwordHash.Verify(pas2);
            Assert.Equal(result, expectedResult);
        }
    }
}