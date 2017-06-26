using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.services;
using UI.services.database.entities;
using UI.services.database.entities.inheritance;

namespace UI.Views.Journey
{
    class JourneyViewModel : Caliburn.Micro.Screen
    {
        private ICarryDataService _context;

        public JourneyViewModel(ICarryDataService context)
        {
            _context = context;
        }

        public IList<object> Journeys {
            get
            {
                return _context.GetJourneys().Select(j =>
                {
                    return new
                    {
                        j.Id,
                        j.Driver,
                        j.Route,
                        j.Transport,
                        j.Date,
                        j.TicketsCount,
                        Summ = j.TicketsCount * j.TicketPrice
                    };
                }).ToList<object>();
            }
        }

        private static T Cast<T>(T typeHolder, Object x)
        {
            // typeHolder above is just for compiler magic
            // to infer the type to cast x to
            return (T)x;
        }

        public void RowSelect(object jou)
        {
            var dummyValueForType = new
            {
                Id = 0,
                Driver = new Driver(),
                Route = new services.database.entities.Route(),
                Transport = new services.database.entities.Transport(),
                Date = new DateTime(),
                TicketsCount = 0,
                Summ = 0f,
            };

            var castedValue = Cast(dummyValueForType, jou);

            var vm = new WindowManager();
            var j = _context.GetJourneyById(castedValue.Id);
            vm.ShowDialog(new JourneyModalViewModel(_context, Update, j), null, null);
        }

        public void Create()
        {
            var vm = new WindowManager();
            vm.ShowDialog(new JourneyModalViewModel(_context, Update), null, null);
        }

        public void Update()
        {
            NotifyOfPropertyChange(() => Journeys);
        }
    }
}
