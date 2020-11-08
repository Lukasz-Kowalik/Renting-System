using RentingSystemAPI.Interfaces;

namespace RentingSystemAPI.DTOs.Request
{
    public class AddItemRequest : IItem
    {
        public int ItemId { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
    }
}