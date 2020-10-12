using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using RentingSystem.Services.Interfaces;
using RentingSystem.Services.Services;
using RentingSystem.Validation;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentingSystem.Models;

namespace RentingSystem
{
    public class Startup
    {
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
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region config

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

            services.AddIdentity<IdentityUser, IdentityRole>(
                    congif =>
                    {
                        congif.Password.RequireDigit = false;
                        congif.Password.RequireLowercase = false;
                        congif.Password.RequireNonAlphanumeric = false;
                        congif.Password.RequireUppercase = false;
                        congif.Password.RequiredLength = 8;
                    })
                //.AddUserManager<UserManager<IdentityUser>>()
                //.AddRoleManager<RoleManager<IdentityRole>>()
                //.AddSignInManager<SignInManager<IdentityUser>>()
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/Account/Logout";
            });

            services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddMvc(option => { option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssemblyContaining<RegisteredUserValidator>()
                );

            //services.AddAuthentication("Cookie")
            //                    .AddCookie("Cookie", config =>
            //                    {
            //                        config.Cookie.Name = "Cookie";
            //                        config.LoginPath = "/Account/Login";
            //                        config.LogoutPath = "/Account/Logout";
            //                    });

            #endregion
        }
    }
}