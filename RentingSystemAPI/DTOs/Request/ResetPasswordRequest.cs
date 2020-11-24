namespace RentingSystemAPI.DTOs.Request
{
    public class ResetPasswordRequest
    {
        public string Password1 { get; set; }
        public string Password2 { get; set; }
        public string Email { get; set; }
    }
}