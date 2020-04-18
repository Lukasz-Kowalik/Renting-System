namespace RentingSystem.Authorization
{
    public class Password
    {
        public Password(string password)
        {
            var passwordHash = new PasswordHash(password);
            Salt = passwordHash.Salt;
            PasswordHash = passwordHash.Hash;
        }

        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}