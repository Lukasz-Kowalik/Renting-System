using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public int Iterations { get; set; }

        [ForeignKey("AccountPermissions")]
        public int AccountTypeId { get; set; }

        [NotMapped]
        public List<Item> ShopingCatr = new List<Item>();

        public AccountPermissions AccountPermissions { get; set; }

        public User(string name, string surname, string email, string password,
            int iterations, AccountPermissions accountPermissions)
        {
            var passwordHash = new PasswordHash(password);
            Name = name;
            Surname = surname;
            Email = email;
            Salt = passwordHash.Salt;
            Iterations = iterations;
            PasswordHash = passwordHash.Hash;
            AccountPermissions = accountPermissions;
        }

        public User()
        {
        }
    }
}