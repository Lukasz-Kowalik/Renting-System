namespace RentingSystemAPI.DTOs.Response
{
    public class ItemListResponse
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}