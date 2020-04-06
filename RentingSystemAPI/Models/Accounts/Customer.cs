namespace DAL.Models
{
    public class Customer : User
    {
        public Customer(string name, string surname, string email, Password password, AccountPermissions accountPermissions)
            : base(name, surname, email, password, accountPermissions)
        {
        }

        public Customer()
        {
        }
    }
}