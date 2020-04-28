using RentingSystem.ViewModels.Authorization;
using System;

namespace RentingSystem.ViewModels.DTOs
{
    public class PasswordDto
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public PasswordDto()
        {
            if (Password == null || ConfirmPassword == null)
            {
                throw new ArgumentException("Value is null");
            }
            if (!Password.Equals(ConfirmPassword))
            {
                throw new ArgumentException("The passwords are different");
            }
            var passwordHash = new PasswordHash(Password);
            Salt = passwordHash.Salt;
            PasswordHash = passwordHash.Hash;
        }

        public PasswordDto(string password, string confirmPassword)
        {
            Password = password;
            ConfirmPassword = confirmPassword;
            if (Password == null || ConfirmPassword == null)
            {
                throw new ArgumentException("Value is null");
            }
            if (!Password.Equals(ConfirmPassword))
            {
                throw new ArgumentException("The passwords are different");
            }
            var passwordHash = new PasswordHash(Password);
            Salt = passwordHash.Salt;
            PasswordHash = passwordHash.Hash;
        }
    }
}