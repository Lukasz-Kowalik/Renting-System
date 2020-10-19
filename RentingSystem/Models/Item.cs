using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
