using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLogic.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsLogin { get; set; } = false;

        [ForeignKey("AccountPermissions")]
        public int AccountTypeId { get; set; }

        public AccountPermissions AccountPermissions { get; set; }
    }
}