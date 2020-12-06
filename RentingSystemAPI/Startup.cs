using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DAL.Initializer;
using RentingSystemAPI.Helpers;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

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
            app.UseRouting();

            app.UseCors(_origins);

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitializeDatabase(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region dbConfig

            // services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

            services.AddCors(options => options.AddPolicy(_origins, builder =>
            {
                builder.WithOrigins(Configuration["Cors:https"],
                        Configuration["Cors:http"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            })
            );
            //Default configuration
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
                                     Password={password};
                                     Trusted_Connection=False;",
                    x => x.MigrationsAssembly("RentingSystemAPI.DAL")));

            #endregion dbConfig

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
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IRentService, RentService>();
            services.AddScoped<IRentedItemsService, RentedItemsService>();
            services.AddScoped<IItemService, ItemService>();

            #endregion Scopes

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentingSystemApi", Version = "v1" });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });
                c.OperationFilter<AuthOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void InitializeDatabase(IApplicationBuilder applicationBuilder)
        {
            Thread.Sleep(30000);
            using var serviceScope = applicationBuilder.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<RentingContext>();
            try
            {
                context.Database.Migrate();
                //Thread.Sleep(60000);
                DbInitializer.Initialize(context);
            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }
    }
}