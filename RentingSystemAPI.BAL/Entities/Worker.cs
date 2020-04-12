namespace RentingSystemAPI.BAL.Entities
{
    public class Worker : Customer
    {
        public Worker(string name, string surname, string email, AccountPermission accountPermission)
            : base(name, surname, email, accountPermission)
        {
        }

        public Worker()
        {
        }
    }
}