using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class RentedItem
    {
        [Key]
        public int RentedItemId { get; set; }

        public int Quantity { get; set; }
        public bool IsReturned { get; set; } = false;
        public int ItemId { get; set; }
        public int RentId { get; set; }
    }
}