using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Views.Transport.Modal
{
    class ModalTransportViewModel : Caliburn.Micro.Screen
    {
        Action<services.database.entities.Transport> _save;
        services.database.entities.Transport transport;

        public ModalTransportViewModel(Action<services.database.entities.Transport> save, services.database.entities.Transport t = null)
        {
            _save = save;

            if (t != null)
            {
                transport = t;
                Name = t.Name;
            }
            else
                transport = new services.database.entities.Transport();
        }

        public string Name
        {
            get
            {
                return transport.Name;
            }
            set
            {
                transport.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void Save()
        {
            _save(transport);
            TryClose();
        }
    }
}
