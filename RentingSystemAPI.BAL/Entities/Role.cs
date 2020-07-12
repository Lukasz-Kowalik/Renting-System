using Microsoft.AspNetCore.Identity;

namespace RentingSystemAPI.BAL.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}