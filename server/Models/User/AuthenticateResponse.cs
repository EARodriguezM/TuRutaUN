namespace TuRutaUN.Models.User
{
    public class AuthenticateResponse
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public byte[] ProfilePicture { get; set; }

        public string Token { get; set; }

        public AuthenticateResponse(TuRutaUN.Entities.Data.User user, string token)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            SecondName = user.SecondName;
            FirstSurname = user.FirstSurname;
            SecondSurname = user.SecondSurname;
            Email = user.Email;
            Mobile = user.Mobile;
            ProfilePicture = user.ProfilePicture;

            Token = token;
        }
    }
}