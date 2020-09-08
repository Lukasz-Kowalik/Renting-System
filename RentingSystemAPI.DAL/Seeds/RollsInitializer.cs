using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System.Collections.Generic;

namespace RentingSystemAPI.DAL.Seeds
{
    public static class RollsInitializer
    {
        public static void Seed(RentingContext context)
        {
            var roles = new List<Role>
            {
                new Role(nameof(AccountTypes.User)),
                new Role(nameof(AccountTypes.Customer)),
                new Role(nameof(AccountTypes.Worker)),
                new Role(nameof(AccountTypes.Admin)),
            };
            context.Roles.AddRangeAsync(roles).GetAwaiter().GetResult();
            context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}