namespace RentingSystemAPI.BAL.Entities
{
    public class Visitor : IVisitor
    {
        public AccountPermission AccountPermission { get; set; }

        public Visitor()
        {
            
        }

        public Visitor(AccountPermission accountPermission)
        {
            AccountPermission = accountPermission;
        }
    }
}