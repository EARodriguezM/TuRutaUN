using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUNAPI.Entities.Data
{
    public partial class Map
    {
        public Map()
        {
            RouteAssigments = new HashSet<RouteAssigment>();
        }

        public long MapId { get; set; }
        public long RouteId { get; set; }
        public long PathId { get; set; }
        public long StageId { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? Status { get; set; }

        public virtual Path Path { get; set; }
        public virtual Route Route { get; set; }
        public virtual Stage Stage { get; set; }
        public virtual ICollection<RouteAssigment> RouteAssigments { get; set; }
    }
}
