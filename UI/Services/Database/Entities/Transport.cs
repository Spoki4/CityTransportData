using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.services.database.entities
{
    public class Transport : inheritance.IIdEntity
    {
        public Transport()
        {
            journeys = new List<Journey>();
        }

        public int Id { get; set; }
        public string Name { set; get; }
        public virtual ICollection<Journey> journeys { get; set; }
    }
}
