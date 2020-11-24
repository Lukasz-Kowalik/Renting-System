using System;

namespace RentingSystem.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public string Name { get; set; }
        public Uri DocumentationURL { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public string Description { get; set; }
    }
}