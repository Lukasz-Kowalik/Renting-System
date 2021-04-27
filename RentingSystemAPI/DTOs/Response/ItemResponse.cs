using System;

namespace RentingSystemAPI.DTOs.Response
{
    public class ItemResponse
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}