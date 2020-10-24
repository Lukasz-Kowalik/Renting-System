using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentingSystemAPI.BAL.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int IdUser { get; set; }
        public string Name { get; set; }
        public int IdItem { get; set; }
        public int Quantity { get; set; }
    }
}