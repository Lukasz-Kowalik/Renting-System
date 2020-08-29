using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Configuration;

namespace RentingSystemAPI.DAL.Context
{
    public class RentingContext : IdentityDbContext<User, Role, int>
    {
        public Microsoft.EntityFrameworkCore.DbSet<Item> Items { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Rent> Rents { get; set; }
        //  public DbSet<AccountPermission> AccountPermissions { get; set; }

        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("AspNetUsers")
                .HasDiscriminator<int>("UserType")
                .HasValue<User>((int)AccountTypes.User)
                .HasValue<Customer>((int)AccountTypes.Customer)
                .HasValue<Worker>((int)AccountTypes.Worker)
                .HasValue<Admin>((int)AccountTypes.Admin);

            modelBuilder.Entity<AccountPermission>()
                .HasOne(ap => ap.User)
                .WithMany()
                .HasForeignKey(p => p.AccountPermissionId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}