using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Views.Route.Modal
{
    class ModalRouteViewModel : Caliburn.Micro.Screen
    {
        private Action<services.database.entities.Route> save;
        private services.database.entities.Route route;

        public ModalRouteViewModel(Action<services.database.entities.Route> save, services.database.entities.Route r = null)
        {
            this.save = save;

            if(r != null)
            {
                route = r;
                Name = r.Name;

            } else
            {
                route = new services.database.entities.Route();
            }
        }
        
        public string Name
        {
            get
            {
                return route.Name;
            }
            set
            {
                route.Name = value;
                NotifyOfPropertyChange(Name);
            }
        }

        public void Save()
        {
            save(route);
            TryClose();
        }
    }
}
