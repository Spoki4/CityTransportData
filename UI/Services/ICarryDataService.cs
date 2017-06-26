using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.services.database.entities;

namespace UI.services
{
    public interface ICarryDataService
    {
        Driver GetDriverById(int id);
        Transport GetTransportById(int id);
        Route GetRouteById(int id);
        Journey GetJourneyById(int id);

        List<Driver> GetDrivers(int offset = 0, int limit = -1);
        List<Transport> GetTransports(int offset = 0, int limit = -1);
        List<Journey> GetJourneys(int offset = 0, int limit = -1);
        List<Route> GetRoutes(int offset = 0, int limit = -1);

        void CreateOrSaveDriver(Driver driver);
        void CreateOrSaveTransport(Transport transport);
        void CreateOrSaveRoute(Route route);
        void AddJourney(Journey journey);
    }
}
