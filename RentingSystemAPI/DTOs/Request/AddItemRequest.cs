namespace RentingSystemAPI.DTOs.Request
{
    public class AddItemRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
    }
}