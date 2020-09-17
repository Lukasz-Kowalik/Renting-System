namespace RentingSystem.ViewModels.Authorization
{
    public class LoggedUser
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}