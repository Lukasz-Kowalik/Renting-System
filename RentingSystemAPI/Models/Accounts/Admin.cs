namespace DAL.Models
{
    public class Admin : Worker
    {
        public Admin(string name, string surname, string email, Password password, AccountPermissions accountPermissions) 
            : base(name, surname, email, password, accountPermissions)
        {
        }

        public Admin()
        {
        }
    }
}