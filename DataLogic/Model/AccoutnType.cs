using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic.Model
{
    public class AccountType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = "Visitor";
        public bool Looking { get; set; } = true;
        public bool Renting { get; set; } = false;
        public bool Resiving { get; set; } = false;
        public bool ChangingPermision { get; set; } = false;

        public AccountType()
        {
        }

        public AccountType(string name, bool looking, bool renting, bool resiving, bool changingPermision)
        {
            Name = name;
            Looking = looking;
            Renting = renting;
            Resiving = resiving;
            ChangingPermision = changingPermision;
        }
    }
}