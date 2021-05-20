using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        public string Name { get; set; }
        public Uri? DocumentationUrl { get; set; }
        public Uri? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ICollection<RentedItem> RentedItems { get; set; }
    }
}