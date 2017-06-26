using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.services.database.entities;

namespace UI.Views.Drivers.Modal
{
    class CreateDriverViewModel : Caliburn.Micro.Screen
    {
        private Action<Driver> _Save;
        private Driver existingDriver;

        public CreateDriverViewModel(Action<Driver> save, Driver d = null)
        {
            _Save = save;
            if (d != null)
            {
                existingDriver = d;
                Fullname = d.Fullname;
                Birthday = d.Birthday;
            }
            else
            {
                existingDriver = new Driver();
                existingDriver.Birthday = DateTime.Now;
            }
        }

        public string Fullname
        {
            get
            {
                return existingDriver.Fullname;
            }
            set
            {
                existingDriver.Fullname = value;
                NotifyOfPropertyChange(() => Fullname);
            }
        }

        public DateTime Birthday
        {
            get
            {
                return existingDriver.Birthday;
            }
            set
            {
                existingDriver.Birthday = value;
                NotifyOfPropertyChange(() => Birthday);
            }
        }

        public void Save()
        {
            _Save(existingDriver);
            TryClose();
        }
    }
}
