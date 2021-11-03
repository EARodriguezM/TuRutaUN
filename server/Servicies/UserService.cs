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
using TuRutaUN.Entities.Data;
using TuRutaUN.Models.User;

namespace TuRutaUN.Servicies
{
    public interface IUserService
    {
       Task<AuthenticateResponse> Authenticate(Models.LoginUser.AuthenticateRequest authenticateRequest);
        Task<User> Register(RegisterRequest registerRequest);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string userId);
        Task Update(UpdateRequest updateRequest);

        Task Delete (string userId);
    }

    public class UserService : IUserService
    {
        //Do global database context, mapper from automapper and my appsettings for only read
        private readonly TuRutaUNContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettigns _appSettings;
        private readonly ILoginService _loginService;

        public UserService(TuRutaUNContext context, IMapper mapper, IOptions<AppSettigns> appSettings, ILoginService loginService)
        {
            //Assign arguments to global readonly variables
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _loginService = loginService;
        }

        #region Public methods

        public async Task<AuthenticateResponse> Authenticate(Models.LoginUser.AuthenticateRequest authenticateRequest)
        {
            var loginUser = await _loginService.Authenticate(authenticateRequest);
            if (loginUser == null) 
                throw new AppException("The user is not UN student");

            var userToFind = _mapper.Map<User>(loginUser);

            //Search user in database with email filter
            var userFinded = await _context.Users.SingleOrDefaultAsync(x => x.UserId == userToFind.UserId);

            if (userFinded == null)
                throw new AppException("The user is not registered in the app");

            //Create a authentication response and generate token to add in the instance to return.
            var authenticateResponse = new AuthenticateResponse(userFinded, GenerateToken(userFinded));

            return authenticateResponse;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task<User> Register(RegisterRequest registerRequest)
        {
            var userInLoginDB = await _loginService.GetById(registerRequest.UserId);

            if (userInLoginDB == null)
                throw new AppException("The user is not UN student");

            var userToRegister = _mapper.Map<User>(registerRequest);

            userToRegister.Email = userInLoginDB.Username+"@unal.edu.co";

            if (await GetById(userToRegister.UserId) != null)
                throw new AppException("The user is already registered");

            if (await _context.Users.AnyAsync(x => x.Mobile == userToRegister.Mobile))
                throw new AppException("The mobile \"" + userToRegister.Mobile + "\" was taken");

            if (string.IsNullOrEmpty(userToRegister.UserTypeId.ToString()))
                throw new AppException("UserType is required");

            await _context.Users.AddAsync(userToRegister);
            await _context.SaveChangesAsync();

            return userToRegister;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var users =  await _context.Users.ToListAsync();
            return users;
        }
        ////////////////////////////////////////////////////////////////////////////////
        public async Task<User> GetById(string userId)
        {
            var userFinded = await _context.Users.FindAsync(userId);
            return userFinded;
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Update(UpdateRequest updateRequest)
        {

            var userWithNewData = _mapper.Map<User>(updateRequest);
            
            var userToUpdate = await GetById(updateRequest.DataUserId);

            if (userToUpdate == null)
                throw new AppException("User not found");

            //Update email if it has changed
            if (!string.IsNullOrWhiteSpace(userWithNewData.Email))
            {
                throw new AppException("The Email can not be updated");
            }

            //Update phone if it has changed
            if (!string.IsNullOrWhiteSpace(userWithNewData.Mobile) && userWithNewData.Mobile != userToUpdate.Mobile)
            {
                //Throw error if the new phone is already taken
                if (_context.Users.Any(x => x.Mobile == userWithNewData.Mobile))
                    throw new AppException("Username " + userWithNewData.Mobile + " is already taken");

                userToUpdate.Mobile = userWithNewData.Mobile;
            }

            if (!string.IsNullOrWhiteSpace(userWithNewData.UserTypeId.ToString()) && userWithNewData.UserTypeId != userToUpdate.UserTypeId)
            {
                userToUpdate.UserTypeId = userWithNewData.UserTypeId;
            }

            //Update user properties if provided
            if (!string.IsNullOrWhiteSpace(userWithNewData.FirstName))
                userToUpdate.FirstName = userWithNewData.FirstName;
            if (!string.IsNullOrWhiteSpace(userWithNewData.SecondName))
                userToUpdate.SecondName = userWithNewData.SecondName;
            if (!string.IsNullOrWhiteSpace(userWithNewData.FirstSurname))
                userToUpdate.FirstSurname = userWithNewData.FirstSurname;
            if (!string.IsNullOrWhiteSpace(userWithNewData.SecondSurname))
                userToUpdate.SecondSurname = userWithNewData.SecondSurname;

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////
        public async Task Delete (string dataUserId)
        {
            var user = await _context.Users.FindAsync(dataUserId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        #endregion


        #region Private helper methods

        //Generate token to communication client-server usings claim with user id and type
        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.UserTypeId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
 
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

        #endregion
    }
}