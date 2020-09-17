using Microsoft.AspNetCore.Identity;

namespace RentingSystem.Models
{
    public sealed class Role : IdentityRole<int>
    {
        public Role()
        {
        }

        public Role(string roleName) : base(roleName)
        {
            NormalizedName = roleName.ToUpper();
        }
        
    }
}