
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using UI.services.database.entities;

namespace UI.services.database
{
    public interface IContext
    {
        IDbSet<Driver> Drivers { get; set; }
        IDbSet<Transport> Transports { get; set; }
        IDbSet<Route> Routes { get; set; }
        IDbSet<Journey> Journeys { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
