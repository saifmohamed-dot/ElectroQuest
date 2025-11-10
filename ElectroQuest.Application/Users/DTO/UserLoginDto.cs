using System.ComponentModel.DataAnnotations;

namespace ElectroQuest.Application.Users.DTO
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
