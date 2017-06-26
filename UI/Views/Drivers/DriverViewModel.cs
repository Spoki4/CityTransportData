using UI.services;
using System.Collections.Generic;
using Caliburn.Micro;
using UI.Views.Drivers.Modal;
using UI.services.database.entities;

namespace UI.Views.Drivers
{
    class DriverViewModel : Caliburn.Micro.PropertyChangedBase
    {
        ICarryDataService CarryData;       

        public DriverViewModel(ICarryDataService context)
        {
            CarryData = context;
        }

        public void OnLoad()
        {

        }

        public void CreateButton()
        {
            var w = new WindowManager();
            w.ShowDialog(new CreateDriverViewModel(CreateOrSaveDriver), null, null);
        }

        public void CreateOrSaveDriver(Driver driver)
        {
            CarryData.CreateOrSaveDriver(driver);
            NotifyOfPropertyChange(() => Drivers);
        }

        public void RowSelect(Driver d)
        {
            var w = new WindowManager();
            w.ShowDialog(new CreateDriverViewModel(CreateOrSaveDriver, d), null, null);
        }

        public List<Driver> Drivers
        {
            get
            {
                if (CarryData == null)
                    return null;
                return CarryData.GetDrivers();
            }
            private set { }
        }
    }
}
