using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingSystemAPI.BAL.Entities;
using System.Collections.Generic;

namespace RentingSystemAPI.DAL.Seeds
{
    public class RollsInitializer : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            var roles = new List<Role>
            {
                new Role {Id = 1,Name = nameof(AccountTypes.User),NormalizedName  = nameof(AccountTypes.User).ToUpper()},
                new Role {Id = 2,Name = nameof(AccountTypes.Customer),NormalizedName  = nameof(AccountTypes.Customer).ToUpper()},
                new Role {Id = 3,Name = nameof(AccountTypes.Worker),NormalizedName  = nameof(AccountTypes.Worker).ToUpper()},
                new Role {Id = 4,Name = nameof(AccountTypes.Admin),NormalizedName  = nameof(AccountTypes.Admin).ToUpper()},
            };
            builder.HasData(roles);
        }
    }
}