using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}