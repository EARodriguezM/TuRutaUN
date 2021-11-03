using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using AutoMapper;

using TuRutaUN.Helpers;
using TuRutaUN.Entities.Login;
using TuRutaUN.Models.LoginUser;

namespace TuRutaUN.Servicies
{
    public interface ILoginService
    {
       Task<LoginUser> Authenticate(AuthenticateRequest authenticateRequest);
        Task<LoginUser> Register(RegisterRequest registerRequest);
        Task<LoginUser> GetById(string userId);

    }

    public class LoginService : ILoginService
    {
        //Do global database context, mapper from automapper and my appsettings for only read
        private readonly ExternalLoginDBContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettigns _appSettings;

        public LoginService(ExternalLoginDBContext context, IMapper mapper, IOptions<AppSettigns> appSettings)
        {
            //Assign arguments to global readonly variables
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        #region Public methods

        public async Task<LoginUser> Authenticate(AuthenticateRequest authenticateRequest)
        {
            //Search user in database with email filter
            var loginUser = await _context.LoginUsers.SingleOrDefaultAsync(x => x.Username == authenticateRequest.Username);

            if (loginUser == null)
                return null;
            
            if (!VerifyPasswordHash(authenticateRequest.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
                throw new AppException("The password is incorrect");
            //Delete passwords from login response and return 
            return loginUser.WithoutPassword();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<LoginUser> Register(RegisterRequest registerRequest)
        {
            registerRequest.Username = registerRequest.Username.ToLower();

            var loginUser = _mapper.Map<LoginUser>(registerRequest);
            var password = registerRequest.Password;

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (await _context.LoginUsers.AnyAsync(x => x.Username == loginUser.Username))
                throw new AppException("Email \"" + loginUser.Username + "\" was taken");

            if (await GetById(loginUser.LoginUserId) != null)
                throw new AppException("Id \"" + loginUser.LoginUserId + "\" is registered");


            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            loginUser.PasswordHash = passwordHash;
            loginUser.PasswordSalt = passwordSalt;

            await _context.LoginUsers.AddAsync(loginUser);
            await _context.SaveChangesAsync();

            return loginUser.WithoutPassword();
        }
        public async Task<IEnumerable<LoginUser>> GetAll()
        {
            var loginUsers =  await _context.LoginUsers.ToListAsync();
            return loginUsers.WithoutPasswords();
        }
        ////////////////////////////////////////////////////////////////////////////////
        public async Task<LoginUser> GetById(string dataUserId)
        {
            var loginUserFinded = await _context.LoginUsers.FindAsync(dataUserId);
            return loginUserFinded.WithoutPassword();
        }
        #endregion


        #region Private helper methods
 
        //Create password hash and salt from password using HMACSHA512
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Verify password in authentication using the stored hash and salt
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        #endregion
    }
}