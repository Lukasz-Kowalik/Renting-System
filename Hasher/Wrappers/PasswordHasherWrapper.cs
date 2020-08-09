using System;
using Hasher.Models;

namespace Hasher.Wrappers
{
    public class PasswordHasherWrapper
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public PasswordHasherWrapper()
        {
        }

        public PasswordHasherWrapper(string password)
        {
            Password = password;
            if (Password == null)
            {
                throw new ArgumentException("Value is null");
            }
            var passwordHash = new PasswordHash(Password);
            Salt = passwordHash.Salt;
            PasswordHash = passwordHash.Hash;
        }

        public PasswordHasherWrapper(string password, string confirmPassword)
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