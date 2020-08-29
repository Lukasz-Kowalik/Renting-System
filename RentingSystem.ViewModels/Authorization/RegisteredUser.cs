namespace RentingSystem.ViewModels.Authorization
{
    public class RegisteredUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        //public byte[] PasswordHash { get; set; }
        //public byte[] Salt { get; set; }
    }
}