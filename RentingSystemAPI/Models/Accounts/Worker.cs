namespace DAL.Models
{
    public class Worker : Customer
    {
        public Worker(string name, string surname, string email, Password password, AccountPermissions accountPermissions)
            : base(name, surname, email, password, accountPermissions)
        {
        }

        public Worker()
        {
        }
    }
}