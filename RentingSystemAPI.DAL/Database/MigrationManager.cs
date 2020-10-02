using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DAL.Seeds;
using System;
using RentingSystemAPI.DAL.Initializer;

namespace RentingSystemAPI.DAL.Database
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<RentingContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return host;
        }
    }
}