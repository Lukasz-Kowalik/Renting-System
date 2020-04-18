using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentingSystemAPI.BAL.Entities;

namespace RentingSystemAPI.BAL.Authorization
{
    [Table("Passwords")]
    public class Password
    {
       [ForeignKey("User")]
        public int PasswordId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public virtual User User { get; set; }
    }
}