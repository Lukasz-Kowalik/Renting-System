using Microsoft.EntityFrameworkCore.Internal;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DAL.Seeds;

namespace RentingSystemAPI.DAL.Initializer
{
    public static class DbInitializer
    {
        public static void Initialize(RentingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            CategorySeed.Seed(context).GetAwaiter().GetResult();
            UserSeed.Seed(context).GetAwaiter().GetResult();
            ItemsSeed.Seed(context).GetAwaiter().GetResult();
            RentsSeed.Seed(context).GetAwaiter().GetResult();
            RentedItemsSeed.Seed(context).GetAwaiter().GetResult();
        }
    }
}