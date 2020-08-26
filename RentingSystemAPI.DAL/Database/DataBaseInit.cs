using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System;
using System.Diagnostics;

namespace RentingSystemAPI.DAL.Database
{
    public static class DatabaseInit
    {
        public static void InitDataBase(IApplicationBuilder app)
        {
            using (var services = app.ApplicationServices.CreateScope())
            {
                SeedData(services.ServiceProvider.GetService<RentingContext>());
            }
        }

        private static void SeedData(RentingContext context)
        {
            try
            {
                Debug.WriteLine("Seeding...");
                if (!((RelationalDatabaseCreator)context.GetService<IDatabaseCreator>()).Exists())
                {
                    context.Roles.AddRange(new[]
                      {
                        new Role(nameof(AccountTypes.User)),
                        new Role(nameof(AccountTypes.Customer)),
                        new Role(nameof(AccountTypes.Worker)),
                        new Role(nameof(AccountTypes.Admin)),
                    });
                    context.SaveChanges();
                }
                Debug.WriteLine("Seeding done!");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}