using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.services;
using UI.Views.Route.Modal;

namespace UI.Views.Route
{
    class RouteViewModel : Caliburn.Micro.Screen
    {
        private ICarryDataService _context;

        public RouteViewModel(ICarryDataService context)
        {
            _context = context;
        }

        public List<services.database.entities.Route> Routes
        {
            get
            {
                return _context.GetRoutes();
            }
            private set { }
        }

        public void Create()
        {
            var wm = new WindowManager();
            wm.ShowDialog(new ModalRouteViewModel(Save), null, null);
        }

        public void RowSelect(services.database.entities.Route route)
        {
            var wm = new WindowManager();
            wm.ShowDialog(new ModalRouteViewModel(Save, route), null, null);
        }

        public void Save(services.database.entities.Route route)
        {
            _context.CreateOrSaveRoute(route);
            NotifyOfPropertyChange(() => Routes);
        }
    }
}
