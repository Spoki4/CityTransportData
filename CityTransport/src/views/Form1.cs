using CityTransport.src.services.database.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CityTransport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (DriverContext db = new DriverContext())
            {
                Driver d1 = new Driver { Name = "Alex" };
                Driver d2 = new Driver { Name = "Jake" };

                db.Drivers.Add(d1);
                db.Drivers.Add(d2);
                db.SaveChanges();

                var drivers = db.Drivers;

                foreach (var driver in drivers)
                {
                    Console.WriteLine("Driver {0} with Name {1}", driver.Id, driver.Name);
                }
            }
        }
    }
}
