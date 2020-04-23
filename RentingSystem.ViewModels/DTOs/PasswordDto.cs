namespace RentingSystem.ViewModels.DTOs
{
    public class PasswordDto
    {
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
