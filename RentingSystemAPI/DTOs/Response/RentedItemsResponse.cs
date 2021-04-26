using System;

namespace RentingSystemAPI.DTOs.Response
{
    public class RentedItemsResponse
    {
        public int RentId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsReturned { get; set; }
        public string Category { get; set; }
        public DateTime RentTime { get; set; }
        public DateTime WhenShouldBeReturned { get; set; }
        public DateTime? RentReturnTime { get; set; }
    }

    public class RentedItemsResponseWithUsers
    {
        public int RentId { get; set; }
        public string Email { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsReturned { get; set; }
        public string Category { get; set; }
        public DateTime RentTime { get; set; }
        public DateTime WhenShouldBeReturned { get; set; }
        public DateTime? RentReturnTime { get; set; }
    }
}