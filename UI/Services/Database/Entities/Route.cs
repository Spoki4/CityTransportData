using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.services.database.entities
{
    public class Route : inheritance.IIdEntity
    {
        public Route()
        {
            journeys = new List<Journey>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Journey> journeys { get; set; }
    }
}
