using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RentingSystemAPI.BAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Rent> Rents { get; set; }

        public ICollection<Cart> Carts { get; set; }
    }
}