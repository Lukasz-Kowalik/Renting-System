using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                  Email = "User@poczta.com",
                  UserName ="User@poczta.com",
                  PasswordHash="AQAAAAEAACcQAAAAEJJP869iaHGZecSbdoXZRVLh3G7tNj9faTUPfzR/b0XTAQnPSi7i/ENeMiFjx/HOYA==",
                  SecurityStamp="5VXTQVJ4YXWZFV52BGXXY7UVXEJGPGRA"
              },  new User
              {
                  FirstName = "Jan",
                  LastName = "Pietrzak",
                  Email = "User2@poczta.com",
                  UserName = "User2@poczta.com",
                  PasswordHash="AQAAAAEAACcQAAAAEJJP869iaHGZecSbdoXZRVLh3G7tNj9faTUPfzR/b0XTAQnPSi7i/ENeMiFjx/HOYA==",
                  SecurityStamp="5VXTQVJ4YXWZFV52BGXXY7UVXEJGPGRA"
              },  new User
              {
                  FirstName = "Mikołaj",
                  LastName = "Dudek",
                  UserName = "Admin2@poczta.com",
                  Email = "Admin2@poczta.com",
                  PasswordHash="AQAAAAEAACcQAAAAEJJP869iaHGZecSbdoXZRVLh3G7tNj9faTUPfzR/b0XTAQnPSi7i/ENeMiFjx/HOYA==",
                  SecurityStamp="5VXTQVJ4YXWZFV52BGXXY7UVXEJGPGRA"
              },  new User
              {
                  FirstName = "Emilia",
                  LastName = "Kasprzak",
                  UserName = "Admin@poczta.com",
                  Email = "Admin@poczta.com",
                  PasswordHash="AQAAAAEAACcQAAAAEJJP869iaHGZecSbdoXZRVLh3G7tNj9faTUPfzR/b0XTAQnPSi7i/ENeMiFjx/HOYA==",
                  SecurityStamp="5VXTQVJ4YXWZFV52BGXXY7UVXEJGPGRA"
              }
          };
            await context.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }
    }
}