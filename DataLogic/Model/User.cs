using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; set; }

        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }

        public AccountTypes AccountType { get; set; }
    }
}