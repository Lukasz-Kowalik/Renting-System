namespace RentingSystemAPI.DTOs.Request
{
    public class DeleteItemRequest
    {
        public int Id { get; set; }
        public int? MaxQuantity { get; set; }
        public int? Quantity { get; set; }
    }
}