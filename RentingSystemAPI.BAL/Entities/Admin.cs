

namespace RentingSystemAPI.BAL.Entities
{
    public class Admin : Worker
    {
        public Admin(string name, string surname, string email, AccountPermission accountPermission) 
            : base(name, surname, email, accountPermission)
        {
        }

        public Admin()
        {
        }
    }
}