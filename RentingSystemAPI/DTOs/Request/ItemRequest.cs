using System;

namespace RentingSystemAPI.DTOs.Request
{
    public class ItemRequest
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public Uri? Url { get; set; }
        public Uri? ImageUrl { get; set; }
        public int MaxQuantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}