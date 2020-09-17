namespace RentingSystemAPI.BAL.Entities
{
    public class AccountPermission
    {
        public int? AccountPermissionId { get; set; }
        public AccountTypes AccountType { get; set; }
        public bool Renting { get; set; }
        public bool Receiving { get; set; }
        public bool ChangingPermission { get; set; }
        public User User { get; set; }
    }
}