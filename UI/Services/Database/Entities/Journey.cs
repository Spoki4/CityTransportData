using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.services.database.entities
{
    public class Journey : inheritance.IIdEntity
    {

        public int Id { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }
        public int DriverId { get; set; }

        [ForeignKey("RouteId")]
        public Route Route { get; set; }
        public int RouteId { get; set; }

        [ForeignKey("TransportId")]
        public Transport Transport { get; set; }
        public int TransportId { get; set; }

        public DateTime Date { get; set; }

        public int TicketsCount { get; set; }
        public float TicketPrice { get; set; }
    }
}
