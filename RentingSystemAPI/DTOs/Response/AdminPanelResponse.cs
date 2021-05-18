using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.DTOs.Response
{
    public class AdminPanelResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int MaxReturnTimeInDays { get; set; }
        public AccountTypes Role { get; set; }
    }
}