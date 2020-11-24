using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.DAL.Seeds
{
    public static class ItemsSeed
    {
        public static async Task Seed(RentingContext context)
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = @"Rezystor SMD 1206 510kΩ",
                    DocumentationURL = new Uri("https://www.cyfronika.com.pl/dokumentacje/smd_r_0402.pdf"),
                    Quantity = 50,
                    MaxQuantity = 50,
                    Description = @"Specyfikacja
                                  - Rezystancja: 510 kΩ
                                  - Tolerancja: 5 %
                                  -Obudowa: SMD 1206"
                },
                new Item
                {
                    Name = @"Dioda LED 5mm RGB WS2811 adresowana",
                    DocumentationURL = new Uri("https://cdn-shop.adafruit.com/datasheets/WS2811.pdf"),
                    Quantity = 70,
                    MaxQuantity = 80,
                    Description = @"Specyfikacja:
                                        -Napięcie zasilania: 5 V
                                        -Pobór prądu If: do 50 mA
                                        -Średnica soczewki: 5 mm
                                        -Możliwość wyboru barwy z 24-bitowej palety
                                        -Sterowana cyfrowo poprzez interfejs 1-wire z możliwością podłączenia wielu urządzeń na jednej linii
                                        -Posiada indywidualny adres urządzenia"
                },
                new Item
                {
                    Name = @"Przewody z haczykami",
                    Quantity = 10,
                    MaxQuantity = 20,
                    Description =
                        @"Zestaw 2 przewodów w kolorze czarnym i czerwonym o długości 25 cm zakończonych złączem z haczykiem. Dzięki konektorom ze sprężynką umożliwiają wielokrotne łączenie różnorakich elementów"
                },
                new Item
                {
                    Name = @"Karta pamięci SanDisk microSD 32GB 80MB/s klasa 10 (bez adaptera) + system NOOBs dla Raspberry Pi 4B/3B+/3B/2B",
                    Quantity = 12,
                    MaxQuantity = 15,
                    Description =
                        @"Karta pamięci microSD klasy 10, który pozwoli na pełne wykorzystanie atutów nowoczesnych smartfonów i tabletów. Urządzenie można wykorzystać jako nośnik pamięci w minikomputerze Raspberry Pi."
                }
            };
            await context.AddRangeAsync(items);
            await context.SaveChangesAsync();
        }
    }
}