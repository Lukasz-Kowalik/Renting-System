namespace RentingSystemAPI.DTOs.Request
{
    public class ReturnItemsRequest
    {
        public int RentId { get; set; }
        public int ItemId { get; set; }
    }
}