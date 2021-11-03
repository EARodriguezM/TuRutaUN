using System.ComponentModel.DataAnnotations;

namespace TuRutaUN.Models.LoginUser
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "ID must be at least 6 characters")]
        public string LoginUserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be at least 3 characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

    }
}