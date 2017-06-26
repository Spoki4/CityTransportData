using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI.services;

namespace UI.Views.PeriodReport
{
    class PeriodReportViewModel : Caliburn.Micro.Screen
    {       
        private ICarryDataService _context;

        public PeriodReportViewModel(ICarryDataService context)
        {
            _context = context;
        }

        private DateTime _ot = DateTime.Now.AddMonths(-1);
        public DateTime Ot
        {
            get
            {
                return _ot;
            }
            set
            {
                _ot = value;
                NotifyOfPropertyChange(() => Ot);
            }
        }

        private DateTime _do = DateTime.Now;
        public DateTime Do
        {
            get
            {
                return _do;
            }
            set
            {
                _do = value;
                NotifyOfPropertyChange(() => Do);
            }
        }
        private string[] vsS =   { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
        "AA", "AB", "AC", "AD", "AE" };
        public void DoIt()
        {
            
            var drivers = _context.GetDrivers();
            var transports = _context.GetTransports();
            var routes = _context.GetRoutes();
            var journeys = _context.GetJourneys();

            journeys = journeys.Where(item => _ot.CompareTo(item.Date) <= 0  && item.Date.CompareTo(_do) <= 0).ToList();

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            try { 
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
                Sheet.Cells[journeys.Count + 2, 5] = "=СУММ(" + vsS[4] + "2:" + vsS[4] + (journeys.Count + 1) + ")";
                Sheet.Cells[journeys.Count + 2, 6] = "=СУММ(" + vsS[5] + "2:" + vsS[5] + (journeys.Count + 1) + ")";
                Sheet.Cells[journeys.Count + 2, 7] = "=СУММ(" + vsS[6] + "2:" + vsS[6] + (journeys.Count + 1) + ")";


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
