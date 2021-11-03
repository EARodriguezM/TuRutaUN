using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUN.Entities.Data
{
    public partial class Route
    {
        public Route()
        {
            Maps = new HashSet<Map>();
        }

        public long RouteId { get; set; }
        public string RouteName { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArriveTime { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Map> Maps { get; set; }
    }
}
