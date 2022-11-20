using System.ComponentModel.DataAnnotations;

namespace LeaveManegementApi.Models
{
    public class UserRegistration
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required,EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //[Required,Compare("Password")]
        //public string ConfirmPassword { get; set; } = string.Empty;
    }
}
