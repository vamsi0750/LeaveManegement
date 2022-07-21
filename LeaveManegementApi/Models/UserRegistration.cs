using System.ComponentModel.DataAnnotations;

namespace LeaveManegementApi.Models
{
    public class UserRegistration
    {
        [Required]
        public string Name { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,MinLength(6)]
        public string Password { get; set; }
        [Required,Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
