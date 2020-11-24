namespace RentingSystem.Models
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }
    }
}