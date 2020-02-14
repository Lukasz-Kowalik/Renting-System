using DataLogic.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public Item Item { get; set; }
        public int Count { get; set; }
        public DateTime RentTime { get; set; }
        public DateTime RentReturn { get; set; }
    }
}