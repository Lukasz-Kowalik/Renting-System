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

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int UserId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
        public string Name { get; set; }
    }
}