using System;
using System.Runtime.Serialization;

namespace SensorApi.Models
{
    public class TemperatureItem
    {
        public long Id { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }

        public long DeviceId { get; set; }
        [IgnoreDataMember]
        public Device Device { get; set; }
    }
}
