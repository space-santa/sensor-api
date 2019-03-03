using SensorApi.Models;
using System;
using System.Linq;

namespace SensorApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TemperatureContext context)
        {
            context.Database.EnsureCreated();

            if (context.Devices.Any())
            {
                return;
            }

            var devices = new Device[]
            {
                new Device{Location="Garage", Name="bt1234"},
                new Device{Location="Living Room", Name="burglgrub"},
                new Device{Location="Stairs", Name="BOULder4"},
            };
            foreach (Device d in devices)
            {
                context.Devices.Add(d);
            }
            context.SaveChanges();

            var temperatureItems = new TemperatureItem[]
            {
                new TemperatureItem{Temperature=12.34, Device=devices[0], Timestamp=DateTime.Parse("2017-12-01 12:34:56")},
                new TemperatureItem{Temperature=13.34, Device=devices[1], Timestamp=DateTime.Parse("2017-12-01 12:35:56")},
                new TemperatureItem{Temperature=14.34, Device=devices[2], Timestamp=DateTime.Parse("2017-12-01 12:36:56")},
                new TemperatureItem{Temperature=15.34, Device=devices[0], Timestamp=DateTime.Parse("2017-12-01 12:37:56")},
                new TemperatureItem{Temperature=16.34, Device=devices[1], Timestamp=DateTime.Parse("2017-12-01 12:38:56")},
                new TemperatureItem{Temperature=17.34, Device=devices[2], Timestamp=DateTime.Parse("2017-12-01 12:39:56")},
                new TemperatureItem{Temperature=18.34, Device=devices[0], Timestamp=DateTime.Parse("2017-12-01 12:40:56")},
                new TemperatureItem{Temperature=19.34, Device=devices[1], Timestamp=DateTime.Parse("2017-12-01 12:41:56")},
                new TemperatureItem{Temperature=20.34, Device=devices[2], Timestamp=DateTime.Parse("2017-12-01 12:42:56")},
            };
            foreach (TemperatureItem t in temperatureItems)
            {
                context.TemperatureItems.Add(t);
            }
            context.SaveChanges();
        }
    }
}
