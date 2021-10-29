using System;
using System.Collections.Generic;

#nullable disable

namespace TuRutaUNAPI.Entities.Data
{
    public partial class Bu
    {
        public Bu()
        {
            RouteAssigments = new HashSet<RouteAssigment>();
        }

        public string BusPlate { get; set; }
        public string Number { get; set; }
        public string Brand { get; set; }
        public string Line { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual ICollection<RouteAssigment> RouteAssigments { get; set; }
    }
}
