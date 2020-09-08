using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DAL.Seeds;
using System;

namespace RentingSystemAPI.DAL.Database
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<RentingContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();

                        if (appContext.Database.EnsureCreated())
                        {
                            RollsInitializer.Seed(appContext);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }

            return host;
        }
    }
}