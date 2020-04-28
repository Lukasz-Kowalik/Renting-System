using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Entities
{
    public class AccountPermission
    {
        [Key, ForeignKey("User")]
        public int AccountPermissionId { get; set; }

        public string AccountType { get; set; } = AccountTypes.Customer.ToString();
        public bool Renting { get; set; } = false;
        public bool Receiving { get; set; } = false;
        public bool ChangingPermission { get; set; } = false;

        public virtual User User { get; set; }

        public AccountPermission()
        {
        }

        public AccountPermission(AccountTypes type, bool looking, bool renting, bool receiving, bool changingPermission)
        {
            AccountType = type.ToString();
            Renting = renting;
            Receiving = receiving;
            ChangingPermission = changingPermission;
        }

        public AccountPermission(AccountTypes typeName)
        {
            switch (typeName)
            {
                case AccountTypes.Customer:
                    {
                        AccountType = AccountTypes.Customer.ToString();
                        Renting = true;
                        Receiving = false;
                        ChangingPermission = false;
                        break;
                    }
                case AccountTypes.Worker:
                    {
                        AccountType = AccountTypes.Worker.ToString();
                        Renting = true;
                        Receiving = true;
                        ChangingPermission = false;
                        break;
                    }
                case AccountTypes.Admin:
                    {
                        AccountType = AccountTypes.Admin.ToString();
                        Renting = true;
                        Receiving = true;
                        ChangingPermission = true;
                        break;
                    }
            }
        }
    }
}