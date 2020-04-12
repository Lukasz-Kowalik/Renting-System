namespace RentingSystemAPI.BAL.Entities
{
    public class Customer : User
    {
        public Customer(string name, string surname, string email, AccountPermission accountPermission)
            : base(name, surname, email, accountPermission)
        {
        }

        public Customer()
        {
        }
    }
}