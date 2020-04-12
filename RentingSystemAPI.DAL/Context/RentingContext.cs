
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.DAL.Context
{
    public class RentingContext:DbContext
    {
        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {

        }
      
        public DbSet<Item> Items { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
