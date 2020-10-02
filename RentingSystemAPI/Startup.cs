using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Helpers;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Services;
using System;
using System.IO;
using System.Reflection;

namespace RentingSystemAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfiguration { get; private set; }
        private readonly string _origins = "_allowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentingSystemAPI");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region dbConfig

            var server = Configuration["DBServer"] ?? "DataBaseSQL";
            var port = Configuration["DBPort"] ?? "1433";
            //using SA isn't good on production, better practice would be using account with lower permission
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "Password2020";
            var database = Configuration["Database"] ?? "Renting";
            services.AddDbContextPool<RentingContext>(options =>
                options.UseSqlServer(
                    @$"Server={server},{port};
                                     Initial Catalog={database};
                                     User ID={user};
                                     Password={password}",
                    x => x.MigrationsAssembly("RentingSystemAPI.DAL")));

            #endregion dbConfig

            services.AddCors(options => options.AddPolicy(_origins, builder =>
                {
                    builder.WithOrigins(Configuration["Cors:https"],
                            Configuration["Cors:http"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                })
            );

            #region authentication

            services.AddIdentity<User, Role>()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<RentingContext>()
                .AddDefaultTokenProviders();

            services.Configure<AppSettings>(Configuration.GetSection("Jwt"));

            #endregion authentication

            #region Scopes

            services.AddScoped<IUserService, UserService>();

            #endregion Scopes

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));

            services.AddControllers();
            // RegisterUserRequest the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentingSystemApi", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}