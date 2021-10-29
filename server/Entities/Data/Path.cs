using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUNAPI.Entities.Data
{
    public partial class Path
    {
        public Path()
        {
            Maps = new HashSet<Map>();
        }

        public long PathId { get; set; }
        public string PathName { get; set; }
        public string PathGpxFile { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Map> Maps { get; set; }
    }
}
