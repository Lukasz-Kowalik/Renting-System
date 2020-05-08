using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        //  public virtual ICollection<User> Users{ get; set; }
        public int MaxReturnTimeInDays { get; set; } = 7;

        public DateTime RentTime { get; set; } = DateTime.Now;

        public DateTime WhenShouldBeReturned { get; set; } = DateTime.Now.AddDays(7);

        public DateTime? RentReturnTime { get; set; } = null;
        public virtual ICollection<RentedItem> RentedItems { get; set; }

        public int UserId { get; set; }

        public Rent()
        {
        }

        //public Rent(int userId, int itemId, int quantity, int maxReturnTimeInDays = 7)
        //{
        //    UserId = userId;
        //    ItemId = itemId;
        //    Quantity = quantity;
        //    MaxReturnTimeInDays = maxReturnTimeInDays;
        //}

        //public Rent(int userId, int itemId, int quantity,
        //    DateTime rentTime, DateTime rentReturnTime, DateTime whenShouldBeReturned)
        //{
        //    UserId = userId;
        //    ItemId = itemId;
        //    Quantity = quantity;
        //    RentTime = rentTime;
        //    RentReturnTime = rentReturnTime;
        //    WhenShouldBeReturned = whenShouldBeReturned;
        //}

        //public Rent(int userId, int itemId, int quantity,
        //    DateTime rentTime, DateTime rentReturnTime
        //   )
        //{
        //    UserId = userId;
        //    ItemId = itemId;
        //    Quantity = quantity;
        //    RentTime = rentTime;
        //    RentReturnTime = rentReturnTime;
        //}

        //public Rent(int userId, int itemId, int quantity,
        //    DateTime rentTime)
        //{
        //    UserId = userId;
        //    ItemId = itemId;
        //    Quantity = quantity;
        //    RentTime = rentTime;
        //}
    }
}