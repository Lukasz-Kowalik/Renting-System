using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
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
                Debug.WriteLine("Applying Migration...");
                if (!((RelationalDatabaseCreator) context.GetService<IDatabaseCreator>()).Exists())
                {
                    context.Database.Migrate();
                }
                Debug.WriteLine("Migration done!");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}