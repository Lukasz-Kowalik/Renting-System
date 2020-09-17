using Microsoft.AspNetCore.Identity;

namespace RentingSystem.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}