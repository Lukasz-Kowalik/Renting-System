using System.ComponentModel.DataAnnotations;

namespace Renting_System.Models
{
    public class Register
    {
       // [Required]
        public string FirstName { get; set; }

      //  [Required]
        public string LastName { get; set; }

      //  [Required]
        public string Email { get; set; }

      //  [Required]
        public string Password { get; set; }

      //  [Required]
        public string PasswordConfirm { get; set; }
    }
}