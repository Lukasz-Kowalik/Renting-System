using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Entities
{
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public Item Item { get; set; }
        public int Quantity { get; set; }
        public int MaxReturnTimeInDays { get; set; } = 7;

        public DateTime RentTime { get; set; } = DateTime.Now;

        public DateTime WhenShouldBeReturned { get; set; } = DateTime.Now.AddDays(7);

        public DateTime? RentReturnTime { get; set; } = null;

        public Rent()
        {
        }

        public Rent(int userId, int itemId, int quantity, int maxReturnTimeInDays = 7)
        {
            UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
            MaxReturnTimeInDays = maxReturnTimeInDays;
        }

        public Rent(int userId, int itemId, int quantity,
            DateTime rentTime, DateTime rentReturnTime, DateTime whenShouldBeReturned)
        {
            UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
            RentTime = rentTime;
            RentReturnTime = rentReturnTime;
            WhenShouldBeReturned = whenShouldBeReturned;
        }

        public Rent(int userId, int itemId, int quantity,
            DateTime rentTime, DateTime rentReturnTime
           )
        {
            UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
            RentTime = rentTime;
            RentReturnTime = rentReturnTime;
        }

        public Rent(int userId, int itemId, int quantity,
            DateTime rentTime)
        {
            UserId = userId;
            ItemId = itemId;
            Quantity = quantity;
            RentTime = rentTime;
        }
    }
}