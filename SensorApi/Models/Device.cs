using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SensorApi.Models
{
    public class Device
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }

        [IgnoreDataMember]
        public List<TemperatureItem> TemperatureItems { get; set; }
    }
}
