using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUN.Entities.Login
{
    public partial class LoginUser
    {
        public string LoginUserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
