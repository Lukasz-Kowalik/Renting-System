using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingSystemAPI.BAL.Authorization
{
    [Table("Passwords")]
    public class Password
    {
       [Key]
        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}