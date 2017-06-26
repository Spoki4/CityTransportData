
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;
using UI.services.database;
using UI.services.database.entities.inheritance;
using UI.services.database.entities;

namespace UI.services
{
    public class CarryDataService : ICarryDataService
    {
        private IContext _context;

        public CarryDataService(IContext context)
        {
            _context = context;
        }

        private C GetById<C>(int id) 
            where C : class, IIdEntity
        {
            return _context.Set<C>().FirstOrDefault(item => item.Id == id);
        }

        private void Add<C>(C entity)
            where C : class, IIdEntity
        {
            var find = GetById<C>(entity.Id);

            if (find != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            else _context.Set<C>().Add(entity);

            _context.SaveChanges();
        }

        private List<C> GetWithOffsetAndLimit<C>(int offset, int limit) 
            where C : class, IIdEntity
        {
            var query = _context.Set<C>();

            if (limit != -1)
                query.Take(limit);

            return query.ToList();
        }

        public Driver GetDriverById(int id) { return GetById<Driver>(id); }
        public Transport GetTransportById(int id) { return GetById<Transport>(id); }
        public Route GetRouteById(int id) { return GetById<Route>(id); }
        public Journey GetJourneyById(int id) { return GetById<Journey>(id); }

        public List<Driver> GetDrivers(int offset = 0, int limit = -1) { return GetWithOffsetAndLimit<Driver>(offset, limit); }
        public List<Transport> GetTransports(int offset = 0, int limit = -1) { return GetWithOffsetAndLimit<Transport>(offset, limit); }
        public List<Route> GetRoutes(int offset = 0, int limit = -1) { return GetWithOffsetAndLimit<Route>(offset, limit); }

        public void CreateOrSaveDriver(Driver driver)
        {
            Add(driver);
        }

        public void CreateOrSaveTransport(Transport transport)
        {
            Add(transport);
        }

        public void CreateOrSaveRoute(Route route)
        {
            Add(route);
        }

        public List<Journey> GetJourneys(int offset = 0, int limit = -1)
        {
            return GetWithOffsetAndLimit<Journey>(offset, limit);
        }

        public void AddJourney(Journey journey)
        {
            Add(journey);
        }
    }
}
