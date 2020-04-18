using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class RentedItem : IItem
    {
        [Key]
        public int RentedItemId { get; set; }

        public int Quantity { get; set; }
        public bool IsReturned { get; set; } = false;
    }
}