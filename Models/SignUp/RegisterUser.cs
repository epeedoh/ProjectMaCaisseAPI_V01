using System.ComponentModel.DataAnnotations;

namespace ProjectMaCaisseAPI_V01.Models.SignUp
{
    public class RegisterUser
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }   

    }
}
