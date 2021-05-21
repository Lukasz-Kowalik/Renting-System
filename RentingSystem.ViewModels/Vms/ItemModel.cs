using System;

namespace RentingSystem.ViewModels.Vms
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Uri? ImageUrl { get; set; }
        public Uri? Url { get; set; }
        public CategoryModel Category { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public string Description { get; set; }
    }
}