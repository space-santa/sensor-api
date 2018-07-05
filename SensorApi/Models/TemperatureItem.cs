using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorApi.Models
{
    public class TemperatureItem
    {
        public long Id { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
