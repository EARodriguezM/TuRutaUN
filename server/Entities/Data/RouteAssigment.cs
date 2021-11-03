using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUN.Entities.Data
{
    public partial class RouteAssigment
    {
        public long RouteAssigmentId { get; set; }
        public string BusPlate { get; set; }
        public long MapId { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual Bus BusPlateNavigation { get; set; }
        public virtual Map Map { get; set; }
    }
}
