using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.DAL.Seeds
{
    public static class RentedItemsSeed
    {
        public static async Task Seed(RentingContext context)
        {
            var rentedItems = new List<RentedItem>
            {
                new RentedItem
                {
                    Quantity = 7,
                    IsReturned = true,
                    ItemId = 1,
                    RentId = 1
                },
                new RentedItem
                {
                    Quantity = 3,
                    IsReturned = true,
                    ItemId = 2,
                    RentId = 2
                },
                new RentedItem
                {
                    Quantity = 5,
                    IsReturned = false,
                    ItemId = 2,
                    RentId = 3
                },
                new RentedItem
                {
                    Quantity = 5,
                    IsReturned = false,
                    ItemId = 2,
                    RentId = 4
                },
                new RentedItem
                {
                    Quantity = 4,
                    IsReturned = false,
                    ItemId = 2,
                    RentId = 5
                },
                new RentedItem
                {
                    Quantity = 4,
                    IsReturned = false,
                    ItemId = 3,
                    RentId = 3
                },
                new RentedItem
                {
                    Quantity = 2,
                    IsReturned = false,
                    ItemId = 3,
                    RentId = 4
                },
                new RentedItem
                {
                    Quantity = 1,
                    IsReturned = false,
                    ItemId = 4,
                    RentId = 4
                },
                new RentedItem
                {
                    Quantity = 1,
                    IsReturned = false,
                    ItemId = 4,
                    RentId = 5
                }
            };
            await context.RentedItems.AddRangeAsync(rentedItems);
            await context.SaveChangesAsync();
        }
    }
}