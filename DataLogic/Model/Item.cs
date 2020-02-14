using System.ComponentModel.DataAnnotations;

namespace DataLogic.Model
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public UrlAttribute DocumentationURL { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}