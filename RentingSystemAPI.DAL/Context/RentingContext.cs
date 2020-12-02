using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Initializer;

namespace RentingSystemAPI.DAL.Context
{
    public class RentingContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<RentedItem> RentedItems { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RollsInitializer());
        }
    }
}