using System.ComponentModel.DataAnnotations;

namespace TuRutaUN.Models.User
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "ID must be at least 6 characters")]
        public string DataUserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be at least 2 characters")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Second name must be at least 2 characters")]
        public string SecondName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 characters")]
        public string FirstSurname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Second surname must be at least 2 characters")]
        public string SecondSurname { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Phone must be at least 10 characters")]
        public string Mobile { get; set; }

        [Required]
        public byte UserTypeId { get; set; }
        
        public byte[] ProfilePicture { get; set; }

    }
}