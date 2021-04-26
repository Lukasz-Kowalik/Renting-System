using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.DAL.Seeds
{
    public static class CategorySeed
    {
        public static async Task Seed(RentingContext context)
        {
            var rentedItems = new List<Category>
            {
                new Category
                {
                  Name="Rezystory"
                },
                new Category
                {
                     Name="Diody"
                },
                new Category
                {
                    Name="Flash"
                },
                new Category
                {
                    Name="Inne"
                }
            };
            await context.Categories.AddRangeAsync(rentedItems);
            await context.SaveChangesAsync();
        }
    }
}