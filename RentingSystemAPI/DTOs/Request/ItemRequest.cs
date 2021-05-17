using System;

namespace RentingSystemAPI.DTOs.Request
{
    public class ItemRequest
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public Uri DocumentationURL { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}