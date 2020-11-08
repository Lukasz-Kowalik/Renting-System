using RentingSystemAPI.Interfaces;

namespace RentingSystemAPI.DTOs.Request
{
    public class RemoveItemFromCartRequest : IItem
    {
        public int ItemId { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
    }
}