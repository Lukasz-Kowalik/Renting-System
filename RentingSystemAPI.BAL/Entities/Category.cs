using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentingSystemAPI.BAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}