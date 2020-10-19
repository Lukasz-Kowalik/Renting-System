using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DAL.Initializer;
using System;
using System.IO;
using System.Threading;

namespace RentingSystemAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //wait for database
            Thread.Sleep(2000);

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //wait to make sure if database was created
                    var context = services.GetRequiredService<RentingContext>();

                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseWebRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>();
                });
    }
}