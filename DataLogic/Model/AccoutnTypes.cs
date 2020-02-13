using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLogic.Model
{
    public class AccountTypes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Looking { get; set; } = true;
        public bool Renting { get; set; } = false;
        public bool Resiving { get; set; } = false;
        public bool ChangingPermision { get; set; } = false;
    }
}
