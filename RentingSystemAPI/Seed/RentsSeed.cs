using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.BAL.Entities;
using System;

namespace RentingSystemAPI.Seed
{
    public static class RentsSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rent>().HasData(
        new Rent
        {
            MaxReturnTimeInDays = 7,
            RentReturnTime = new DateTime(2019, 01, 20),
            WhenShouldBeReturned = new DateTime(2020, 04, 07),
            RentTime = new DateTime(2019, 02, 01),
            UserId = 1
        },
        new Rent
        {
            MaxReturnTimeInDays = 7,
            RentReturnTime = new DateTime(2019, 01, 20),
            WhenShouldBeReturned = new DateTime(2019, 04, 07, 11, 13, 26),
            RentTime = new DateTime(2019, 01, 24),
            UserId = 1
        },
        new Rent
        {
            MaxReturnTimeInDays = 7,
            RentTime = new DateTime(2020, 01, 20),
            WhenShouldBeReturned = new DateTime(2020, 04, 07, 11, 1, 26),
            UserId = 3
        },
        new Rent
        {
            MaxReturnTimeInDays = 10,
            RentTime = new DateTime(2020, 03, 31, 11, 13, 26),
            WhenShouldBeReturned = new DateTime(2020, 04, 07, 11, 13, 26),
            UserId = 2
        },
        new Rent
        {
            MaxReturnTimeInDays = 7,
            RentTime = new DateTime(2020, 03, 31, 11, 13, 26),
            WhenShouldBeReturned = new DateTime(2020, 04, 07, 11, 13, 26),
            UserId = 1
        });
        }
    }
}