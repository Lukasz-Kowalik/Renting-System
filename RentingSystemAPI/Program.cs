using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;

namespace RentingSystemAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Thread.Sleep(10000);
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseWebRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>();
                });
    }
}