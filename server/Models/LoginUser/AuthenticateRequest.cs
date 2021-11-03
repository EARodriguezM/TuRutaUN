using System.ComponentModel.DataAnnotations;

namespace TuRutaUN.Models.LoginUser
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
    }
}