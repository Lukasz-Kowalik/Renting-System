using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        public int MaxReturnTimeInDays { get; set; } = 7;

        public DateTime RentTime { get; set; } = DateTime.Now;

        public DateTime WhenShouldBeReturned { get; set; } = DateTime.Now.AddDays(7);

        public DateTime? RentReturnTime { get; set; } = null;
        public ICollection<RentedItem> RentedItems { get; set; }

        public int UserId { get; set; }
    }
}