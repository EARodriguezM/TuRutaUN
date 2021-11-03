using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUN.Entities.Data
{
    public partial class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool? Status { get; set; }
        public byte UserTypeId { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
