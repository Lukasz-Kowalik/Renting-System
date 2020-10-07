﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.BAL.Authorization
{
    [Table("Passwords")]
    public class Password
    {
       [Key,ForeignKey("User")]
        public int PasswordId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public User User { get; set; }
    }
}