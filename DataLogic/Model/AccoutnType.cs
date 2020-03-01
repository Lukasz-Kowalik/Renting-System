using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic.Model
{
    public class AccountPermissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AccountTypeName { get; set; } = AccountTypes.Name.Visitor.ToString();
        public bool Looking { get; set; } = true;
        public bool Renting { get; set; } = false;
        public bool Resiving { get; set; } = false;
        public bool ChangingPermision { get; set; } = false;

        public AccountPermissions()
        {
        }

        public AccountPermissions(AccountTypes.Name type, bool looking, bool renting, bool resiving, bool changingPermision)
        {
            AccountTypeName = type.ToString();
            Looking = looking;
            Renting = renting;
            Resiving = resiving;
            ChangingPermision = changingPermision;
        }

        public AccountPermissions(AccountTypes.Name typeName)
        {
            switch (typeName)
            {
                case AccountTypes.Name.Customer:
                    {
                        AccountTypeName = AccountTypes.Name.Customer.ToString();
                        Looking = true;
                        Renting = true;
                        Resiving = false;
                        ChangingPermision = false;
                        break;
                    }
                case AccountTypes.Name.Worker:
                    {
                        AccountTypeName = AccountTypes.Name.Worker.ToString();
                        Looking = true;
                        Renting = true;
                        Resiving = true;
                        ChangingPermision = false;
                        break;
                    }
                case AccountTypes.Name.Admin:
                    {
                        AccountTypeName = AccountTypes.Name.Admin.ToString();
                        Looking = true;
                        Renting = true;
                        Resiving = true;
                        ChangingPermision = true;
                        break;
                    }
            }
        }
    }
}