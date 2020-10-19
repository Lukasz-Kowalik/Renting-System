using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentingSystem.Models;

namespace RentingSystem
{
    public class AppDbContext : IdentityDbContext<User,Role,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                 : base(options)
        {
        }
    }
}