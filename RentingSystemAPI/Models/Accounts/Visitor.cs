namespace DAL.Models
{
    public class Visitor : IVisitor
    {
        public AccountPermissions AccountPermissions { get; set; }

        public Visitor()
        {
            
        }

        public Visitor(AccountPermissions accountPermissions)
        {
            AccountPermissions = accountPermissions;
        }
    }
}