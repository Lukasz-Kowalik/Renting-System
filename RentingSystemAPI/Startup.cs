using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentingSystemAPI.Model;

namespace RentingSystemAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"] ?? "DataBaseSQL";
            var port = Configuration["DBPort"] ?? "1433";
            //using SA isn't good on production, better practice would be using account with lower permission
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "Password2020";
            var database = Configuration["Database"] ?? "Renting";
            services.AddDbContext<RentingContext>(options => options.UseSqlServer(
                                                                                    $"Server={server},{port};" +
                                                                                               $"Initial Catalog={database};" +
                                                                                               $"User ID={user};" +
                                                                                               $"Password={password}"));
            services.AddDbContext<RentingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("RentingDb")));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DatabaseInit.InitDataBase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}