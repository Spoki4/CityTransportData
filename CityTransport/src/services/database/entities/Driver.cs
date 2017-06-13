using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CityTransport.src.services.database.entities
{
    class Driver
    {
        public int Id;
        public String Name;
    }
    
    class DriverContext : DbContext
    {
        public DriverContext() : base("DatabaseConnection")
        {

        }

        public DbSet<Driver> Drivers;
    }
}
