using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;

namespace RentingSystemAPI.DAL.Seeds
{
    public static class UserSeed

    {
        public static async Task Seed(RentingContext context)
        {
            var users = new List<User>
          {
              new User
              {
                  FirstName = "Adam",
                  LastName = "Kruk",
                  Email = "akruk@poczta.com",
                  UserName ="akruk@poczta.com"
              },  new User
              {
                  FirstName = "Jan",
                  LastName = "Pietrzak",
                  Email = "jpietrzak@poczta.com",
                  UserName = "jpietrzak@poczta.com"
              },  new User
              {
                  FirstName = "Mikołaj",
                  LastName = "Dudek",
                  UserName = "mdudek@poczta.com",
                  Email = "mdudek@poczta.com"
              },  new User
              {
                  FirstName = "Emilia",
                  LastName = "Kasprzak",
                  UserName = "ekasprzyk@poczta.com",
                  Email = "ekasprzyk@poczta.com"
              }
          };
            await context.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }
    }
}