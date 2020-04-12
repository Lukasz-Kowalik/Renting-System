using System.ComponentModel.DataAnnotations;

namespace RentingSystem.Authorization
{
    public class Password
    {
        public Password(string password)
        {
            var passwordHash = new PasswordHash(password);
            Salt = passwordHash.Salt;
            PasswordHash = passwordHash.Hash;
        }

        [Key]
        public int Id { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}