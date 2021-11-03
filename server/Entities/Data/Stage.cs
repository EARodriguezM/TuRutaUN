using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUN.Entities.Data
{
    public partial class Stage
    {
        public Stage()
        {
            Maps = new HashSet<Map>();
        }

        public long StageId { get; set; }
        public string StageName { get; set; }
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Map> Maps { get; set; }
    }
}
