using System.ComponentModel.DataAnnotations;

namespace ElectroQuest.Application.Users.DTO
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Required")]
        [Compare("Password" , ErrorMessage = "Passwords Must Match")]
        public string PasswordConfirmation { get; set; }
    }
}
