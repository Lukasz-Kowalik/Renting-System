using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Password
    {
        public Password(string password)
        {
            var passwordHash = new PasswordHash(password);
            Salt = passwordHash.Salt;
            PasswordHash = passwordHash.Hash;
        }

        public Password()
        {
        }

        [Key]
        public int Id { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}