using System;
using System.Data.Entity;
using UI.services.database.entities;

namespace UI.services.database
{
    public class ApplicationDatabase : DbContext, IContext
    {
        public virtual IDbSet<Driver> Drivers { get; set; }
        public virtual IDbSet<Transport> Transports { get; set; }
        public virtual IDbSet<Route> Routes { get; set; }
        public virtual IDbSet<Journey> Journeys { get; set; }

        public ApplicationDatabase() : base("DatabaseConnection")
        {}
    }
}
