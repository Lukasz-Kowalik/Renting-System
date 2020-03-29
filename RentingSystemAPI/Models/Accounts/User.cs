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

        [ForeignKey("Password")]
        public int PasswordId { get; set; }

        public Password Password { get; set; }

        [ForeignKey("AccountPermissions")]
        public int AccountTypeId { get; set; }

        [NotMapped]
        public HashSet<Item> ShopingCatr = new HashSet<Item>();


        public AccountPermissions AccountPermissions { get; set; }


        public User(string name, string surname, string email, Password password, AccountPermissions accountPermissions)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            AccountPermissions = accountPermissions;
        }
        
        public User()
        {
            
        }
    }
}