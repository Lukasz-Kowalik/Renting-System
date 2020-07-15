using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Entities
{
    public class User : IdentityUser<int>
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AccountPermission AccountPermission { get; set; }
        public ICollection<Rent> Rents { get; set; }
        public int UserType { get; set; }
        public string Salt { get; set; }
        
        [NotMapped]
        public List<Item> ShoppingCart = new List<Item>();
    }
}