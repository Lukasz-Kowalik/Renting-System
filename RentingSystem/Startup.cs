using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using RentingSystem.Services.Interfaces;
using RentingSystem.Services.Services;
using RentingSystem.Validation;
using System;
using System.Security.Claims;
using RentingSystem.Models;

namespace RentingSystem
{
    public class Startup
    {
        private string _origins = "CORS";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePages("text/html",
                    "<h1>Status code page</h1> <h2>Status Code: {0}</h2>");
                //   The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseCors(_origins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region config

            services.AddCors(options => options.AddPolicy(_origins, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                })
            );

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });

            services.AddScoped<DbContext, AppDbContext>();
            services.AddHttpClient("API Client", client =>
            {
                client.BaseAddress = new Uri(Configuration["Container:AddressAPI"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUserService, UserService>();

            services.AddIdentity<User, Role>(
                    config =>
                    {
                        config.Password.RequireDigit = false;
                        config.Password.RequireLowercase = false;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequireUppercase = false;
                        config.Password.RequiredLength = 8;
                    })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/Account/Logout";
                config.AccessDeniedPath = "/Account/Error";
                config.Cookie.HttpOnly = false;
            });

            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, Role>>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc(option => { option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssemblyContaining<RegisteredUserValidator>()
                );

            //services.AddAuthorization(config =>
            //{
            //    //  config.AddPolicy("All", policy => policy.RequireClaim("id"));
            //    config.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            //    config.AddPolicy("Customer", policy => policy.RequireClaim(ClaimTypes.Role, "Customer"));
            //    config.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
            //    config.AddPolicy("Worker", policy => policy.RequireClaim(ClaimTypes.Role, "Worker"));
            //});

            #endregion
        }
    }
}