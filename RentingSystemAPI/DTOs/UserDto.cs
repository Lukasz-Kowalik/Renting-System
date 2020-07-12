namespace RentingSystemAPI.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        //public string UserName
        //{
        //    get => UserName;
        //    set => UserName = Email;
        //}
    }
}