using DataLogic.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace RentingSystemAPI.Model
{
    public static class DatabaseInit
    {
        public static void InitDataBase(IApplicationBuilder app)
        {
            using (var services = app.ApplicationServices.CreateScope())
            {
                SeedData(services.ServiceProvider.GetService<RentingContext>());
            }
        }

        private static void SeedData(RentingContext context)
        {
            System.Console.WriteLine("Appling Migration...");
            context.Database.Migrate();
            SeedAccountTypes(context);
            SeedItems(context);
            System.Console.WriteLine("Migration done!");
        }

        private static void SeedAccountTypes(RentingContext context)
        {
            System.Console.WriteLine("Adding Account types...");
            if (!context.AccountTypes.Any())
            {
                context.AccountTypes.AddRange(
                   new AccountPermissions(AccountTypes.Name.Visitor),
                   new AccountPermissions(AccountTypes.Name.Customer),
                   new AccountPermissions(AccountTypes.Name.Worker),
                   new AccountPermissions(AccountTypes.Name.Admin)
                );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Account types already exists.");
            }
        }

        private static void SeedItems(RentingContext context)
        {
            System.Console.WriteLine("Adding Items...");
            if (!context.Items.Any())
            {
                context.Items.AddRange(
                  new Item
                  {
                      Count = 50,
                      Name = "Rezystor SMD 1206 510kΩ",
                      Description = @"Specyfikacja
                                       -Rezystancja: 510 kΩ
                                       -Tolerancja: 5 %
                                       -Obudowa: SMD 1206",
                      DocumentationURL = @$"https://www.cyfronika.com.pl/dokumentacje/smd_r_0402.pdf"
                  },
                  new Item
                  {
                      Count = 80,
                      Name = "Dioda LED 5mm RGB WS2811 adresowana",
                      Description = @$"Specyfikacja:
                                        -Napięcie zasilania: 5 V
                                        -Pobór prądu If: do 50 mA
                                        -Średnica soczewki: 5 mm
                                        -Możliwość wyboru barwy z 24-bitowej palety
                                        -Sterowana cyfrowo poprzez interfejs 1-wire z możliwością podłączenia wielu urządzeń na jednej linii
                                        -Posiada indywidualny adres urządzenia",
                      DocumentationURL = @$"https://cdn-shop.adafruit.com/datasheets/WS2811.pdf"
                  },
                  new Item
                  {
                      Count = 20,
                      Name = "Przewody z haczykami",
                      Description = @$"Zestaw 2 przewodów w kolorze czarnym i czerwonym o długości 25 cm zakończonych złączem z haczykiem. Dzięki konektorom ze sprężynką umożliwiają wielokrotne łączenie różnorakich elementów",
                      DocumentationURL = null
                  }, new Item
                  {
                      Count = 15,
                      Name = "Karta pamięci SanDisk microSD 32GB 80MB/s klasa 10 (bez adaptera) + system NOOBs dla Raspberry Pi 4B/3B+/3B/2B",
                      Description = @$"Karta pamięci microSD klasy 10, który pozwoli na pełne wykorzystanie atutów nowoczesnych smartfonów i tabletów. Urządzenie można wykorzystać jako nośnik pamięci w minikomputerze Raspberry Pi.",
                      DocumentationURL = null
                  }
               );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Items already exists.");
            }
        }

        private static void SeedRents(RentingContext context)
        {
            System.Console.WriteLine("Adding Rents...");
            if (!context.Rents.Any())
            {
                context.Rents.AddRange(

               );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Rents already exists.");
            }
        }

        private static void SeedUsers(RentingContext context)
        {
            System.Console.WriteLine("Adding Users...");
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Name = "Adam",
                        Surname = "Kruk",
                        Email = "akruk@poczta.com",
                        PasswordHash = "",
                        AccountPermissions = new AccountPermissions(AccountTypes.Name.Visitor)
                    },
                    new User
                    {
                        Name = "Jan",
                        Surname = "Pietrzak",
                        Email = "jpietrzak@poczta.com",
                        PasswordHash = "",
                        AccountPermissions = new AccountPermissions(AccountTypes.Name.Admin)
                    }
               );
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Users already exists.");
            }
        }
    }
}