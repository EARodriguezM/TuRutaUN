using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUNAPI.Entities.Login
{
    public partial class LoginUser
    {
        public byte LoginUserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
