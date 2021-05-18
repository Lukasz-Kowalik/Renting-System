using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        public DateTime RentTime { get; set; } = DateTime.Now;

        public DateTime WhenShouldBeReturned { get; set; } = DateTime.Now.AddDays(Constants.MaxDefaultReturnTime);

        public DateTime? RentReturnTime { get; set; } = null;
        public ICollection<RentedItem> RentedItems { get; set; }

        public int UserId { get; set; }
    }
}