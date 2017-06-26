using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.services;
using UI.services.database.entities;

namespace UI.Views.Journey
{
    class JourneyModalViewModel : Caliburn.Micro.Screen
    {
        private ICarryDataService _context;
        private services.database.entities.Journey _journey;
        private Action saved;

        public JourneyModalViewModel(
            ICarryDataService context,
            Action Saved,
            services.database.entities.Journey j = null
            )
        {
            _context = context;
            saved = Saved;
            if (j != null)
            {
                _journey = j;
                DriverVal = j.Driver;
                TransportVal = j.Transport;
                RouteVal = j.Route;
                Date = j.Date;
                TicketCount = j.TicketsCount;
                TicketPrice = j.TicketPrice;
            }
            else
            {
                _journey = new services.database.entities.Journey();
                _journey.Date = DateTime.Now;
            }
        }

        public List<Driver> Drivers {
            get
            {
                return _context.GetDrivers();
            }
        }
        public List<services.database.entities.Transport> Transports
        {
            get
            {
                return _context.GetTransports();
            }
        }
        public List<services.database.entities.Route> Routes
        {
            get
            {
                return _context.GetRoutes();
            }
        }
        
        public int TicketCount
        {
            get
            {
                return _journey.TicketsCount;
            }
            set
            {
                _journey.TicketsCount = value;
                NotifyOfPropertyChange(() => TicketCount);
            }
        }
        public float TicketPrice
        {
            get
            {
                return _journey.TicketPrice;
            }
            set
            {
                _journey.TicketPrice = value;
                NotifyOfPropertyChange(() => TicketPrice);
            }
        }

        public Driver DriverVal
        {
            get
            {
                return _journey.Driver;
            }
            set
            {
                _journey.Driver = value;
                NotifyOfPropertyChange(() => DriverVal);
            }
        }
        
        public services.database.entities.Transport TransportVal
        {
            get
            {
                return _journey.Transport;
            }
            set
            {
                _journey.Transport = value;
                NotifyOfPropertyChange(() => TransportVal);
            }
        }
        
        public services.database.entities.Route RouteVal
        {
            get
            {
                return _journey.Route;
            }
            set
            {
                _journey.Route = value;
                NotifyOfPropertyChange(() => RouteVal);
            }
        }
        
        public DateTime Date
        {
            get
            {
                return _journey.Date;
            }
            set
            {
                _journey.Date = value;
                NotifyOfPropertyChange(() => Date);
            }
        }

        public void Save()
        {
            _context.AddJourney(_journey);
            saved();
            TryClose();
        }
    }
}
