﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace RentingSystemAPI.Model
{
    public class RentingContext:DbContext
    {
        public RentingContext(DbContextOptions<RentingContext> options) : base(options)
        {

        }
        public DbSet<AccountPermissions> AccountsPermissions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
