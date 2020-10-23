using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DAL.Initializer;

namespace RentingSystemAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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