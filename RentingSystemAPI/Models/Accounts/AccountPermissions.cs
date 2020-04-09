using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class AccountPermissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AccountType { get; set; } = AccountTypes.Visitor.ToString();
        public bool Looking { get; set; } = true;
        public bool Renting { get; set; } = false;
        public bool Receiving { get; set; } = false;
        public bool ChangingPermission { get; set; } = false;

        public AccountPermissions()
        {
        }

        public AccountPermissions(AccountTypes type, bool looking, bool renting, bool receiving, bool changingPermission)
        {
            AccountType = type.ToString();
            Looking = looking;
            Renting = renting;
            Receiving = receiving;
            ChangingPermission = changingPermission;
        }

        public AccountPermissions(AccountTypes typeName)
        {
            switch (typeName)
            {
                case AccountTypes.Customer:
                    {
                        AccountType = AccountTypes.Customer.ToString();
                        Looking = true;
                        Renting = true;
                        Receiving = false;
                        ChangingPermission = false;
                        break;
                    }
                case AccountTypes.Worker:
                    {
                        AccountType = AccountTypes.Worker.ToString();
                        Looking = true;
                        Renting = true;
                        Receiving = true;
                        ChangingPermission = false;
                        break;
                    }
                case AccountTypes.Admin:
                    {
                        AccountType = AccountTypes.Admin.ToString();
                        Looking = true;
                        Renting = true;
                        Receiving = true;
                        ChangingPermission = true;
                        break;
                    }
            }
        }
    }
}