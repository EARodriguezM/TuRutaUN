using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUNAPI.Entities.Data
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public byte UserTypeId { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
