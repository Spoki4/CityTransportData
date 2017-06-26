using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.services;
using UI.Views.Transport.Modal;

namespace UI.Views.Transport
{
    class TransportViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private ICarryDataService _context;

        public TransportViewModel(ICarryDataService context)
        {
            _context = context;
        }

        public List<services.database.entities.Transport> Transports
        {
            get
            {
                return _context.GetTransports();
            }
            private set { }
        }

        public void Create()
        {
            var wm = new WindowManager();
            wm.ShowDialog(new ModalTransportViewModel(Save), null, null);
        }

        public void RowSelect(services.database.entities.Transport transport)
        {
            var wm = new WindowManager();
            wm.ShowDialog(new ModalTransportViewModel(Save, transport), null, null);
        }

        public void Save(services.database.entities.Transport transport)
        {
            _context.CreateOrSaveTransport(transport);
            NotifyOfPropertyChange(() => Transports);
        }
    }
}
