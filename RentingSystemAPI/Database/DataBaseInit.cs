using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

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
            try
            {
                System.Console.WriteLine("Appling Migration...");
                context.Database.Migrate();
                SeedItems(context);
                SeedUsers(context);
                SeedRents(context);
                System.Console.WriteLine("Migration done!");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
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
                         Quantity = 50,
                         Name = "Rezystor SMD 1206 510kΩ",
                         Description = @"Specyfikacja
                                       -Rezystancja: 510 kΩ
                                       -Tolerancja: 5 %
                                       -Obudowa: SMD 1206",
                         DocumentationURL = @$"https://www.cyfronika.com.pl/dokumentacje/smd_r_0402.pdf"
                     },
                     new Item
                     {
                         Quantity = 80,
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
                         Quantity = 20,
                         Name = "Przewody z haczykami",
                         Description = @$"Zestaw 2 przewodów w kolorze czarnym i czerwonym o długości 25 cm zakończonych złączem z haczykiem. Dzięki konektorom ze sprężynką umożliwiają wielokrotne łączenie różnorakich elementów",
                         DocumentationURL = null
                     }, new Item
                     {
                         Quantity = 15,
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
                    new Rent
                    (
                        1,
                         2,
                         5
                    ),
                    new Rent
                   (
                         2,
                         1,
                        5,
                         10
                    ),
                    new Rent
                    (
                        3,
                        3,
                        2,
                         new DateTime(2020, 1, 20)
                    ), new Rent
                    (
                         1,
                         3,
                         13,
                         new DateTime(2019, 1, 20),
                         new DateTime(2019, 1, 24)

                   ), new Rent
                    (
                         1,
                         3,
                        13,
                         new DateTime(2019, 1, 20),
                        new DateTime(2019, 2, 1)

                    )
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
                    new User(
                 "Adam",
                 "Kruk",
                 "akruk@poczta.com",
                 new Password("password"),
                 new AccountPermissions(AccountTypes.Name.Visitor)
                 ),
             new User
               (
                    "Jan",
                   "Pietrzak",
                    "jpietrzak@poczta.com",
                    new Password("password"),
                    new AccountPermissions(AccountTypes.Name.Customer)
              ),
                new User
                (
                    "Mikołaj",
                     "Dudek",
                    "mdudek@poczta.com",
                    new Password("password"),
                     new AccountPermissions(AccountTypes.Name.Worker)
                ), new User
                (
                     "Emilia",
                     "Kasprzak",
                     "ekasprzyk@poczta.com",
                     new Password("password"),
                     new AccountPermissions(AccountTypes.Name.Admin)
                )
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