using RentingSystemAPI.BAL.Authorization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        [Required]
        public virtual Password Password { get; set; }
        [Required]
        public virtual  AccountPermission AccountPermission { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        [NotMapped]
        public List<Item> ShoppingCart = new List<Item>();

        public User(string name, string surname, string email, AccountPermission accountPermission)
        {
            Name = name;
            Surname = surname;
            Email = email;
            AccountPermission = accountPermission;
        }

        public User()
        {
            Rents = new List<Rent>();
        }
    }
}