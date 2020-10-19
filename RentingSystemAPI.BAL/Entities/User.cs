using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // public AccountPermission AccountPermission { get; set; }
        public ICollection<Rent> Rents { get; set; }

        //[JsonIgnore]
        //public string RefreshToken { get; set; }

        [NotMapped] public Cart Cart { get; set; }
    }
}