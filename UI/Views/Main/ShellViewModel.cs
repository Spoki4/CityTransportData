using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using UI.services;
using UI.services.database;
using UI.Views.Drivers;
using UI.Views.Journey;
using UI.Views.PeriodReport;
using UI.Views.Route;
using UI.Views.Transport;

namespace UI.Views.Main {
    public class ShellViewModel : Caliburn.Micro.Screen, IShell
    {
        IWindowManager _windowManager;
        volatile ICarryDataService _context;

        public ShellViewModel()
        {
            _windowManager = new WindowManager();
            new Thread(() =>
            {
                _context = new CarryDataService(new ApplicationDatabase());
                _context.GetDrivers();
                _context.GetRoutes();
                _context.GetTransports();
                OnUIThread(() =>
                {
                    IsLoaded = true;
                });
            }).Start();
        }

        private bool _loaded = false;

        public bool IsLoaded
        {
            get
            {
                return _loaded;
            }
            set
            {
                _loaded = value;
                NotifyOfPropertyChange(() => IsLoaded);
                NotifyOfPropertyChange(() => LoadingVisible);
                NotifyOfPropertyChange(() => ContentVisible);
            }
        }

        public Visibility LoadingVisible
        {
            get
            {
                return IsLoaded ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ContentVisible
        {
            get
            {
                return IsLoaded ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public void ExitMenuButton()
        {
            Application.Current.Shutdown();
        }

        public void DriverMenuButton()
        {
            _windowManager.ShowDialog(new DriverViewModel(_context), null, null);
        }

        public void TransportMenuButton()
        {
            _windowManager.ShowDialog(new TransportViewModel(_context), null, null);
        }

        public void RouteMenuButton()
        {
            _windowManager.ShowDialog(new RouteViewModel(_context), null, null);
        }

        public void JourneyMenuButton()
        {
            _windowManager.ShowDialog(new JourneyViewModel(_context), null, null);
        }

        public void PeriodReport()
        {
            _windowManager.ShowDialog(new PeriodReportViewModel(_context), null, null);
        }

        public void TicketReport()
        {
            var drivers = _context.GetDrivers();
            var transports = _context.GetTransports();
            var routes = _context.GetRoutes();
            var journeys = _context.GetJourneys();

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                Microsoft.Office.Interop.Excel.Workbook Book;
                Microsoft.Office.Interop.Excel.Worksheet Sheet;
                //Книга.
                Book = xlApp.Workbooks.Add(System.Reflection.Missing.Value);
                //Таблица.
                Sheet = (Microsoft.Office.Interop.Excel.Worksheet)Book.Sheets[1];


                Sheet.Cells[1, 1] = "Водитель";
                Sheet.Cells[1, 2] = "Путь";
                Sheet.Cells[1, 3] = "Транспорт";
                Sheet.Cells[1, 4] = "Дата";
                Sheet.Cells[1, 5] = "Количество билетов";
                Sheet.Cells[1, 6] = "Цена за билет";
                Sheet.Cells[1, 7] = "Сумма";

                for (int i = 0; i < journeys.Count; i++)
                {
                    Sheet.Cells[i + 2, 1] = journeys[i].Driver.Fullname;
                    Sheet.Cells[i + 2, 2] = journeys[i].Route.Name;
                    Sheet.Cells[i + 2, 3] = journeys[i].Transport.Name;
                    Sheet.Cells[i + 2, 4] = journeys[i].Date;
                    Sheet.Cells[i + 2, 5] = journeys[i].TicketsCount;
                    Sheet.Cells[i + 2, 6] = journeys[i].TicketPrice;
                    Sheet.Cells[i + 2, 7] = journeys[i].TicketPrice * journeys[i].TicketsCount;
                }


                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    Book.SaveAs(saveFileDialog.FileName);
                }

                TryClose();
            }
            catch (Exception e)
            {

            }
            finally
            {
                xlApp.Quit();
            }
        }
    } 
}