using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentingSystemAPI.BAL.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public User User { get; set; }

        public ICollection<Item> Items { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; }
    }
}