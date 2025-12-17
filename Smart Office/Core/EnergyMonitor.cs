using Smart_Office.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Core
{
    public delegate bool DeviceFilter(ISmartControl device);

    public class EnergyMonitor
    {
        public double CalculateTotalConsumption(IEnumerable<IPowerConsuming> consumers)
        {
            double total = 0;
            foreach (var consumer in consumers)
            {
                total += consumer.CurrentPowerUsage;
            }
            return total;
        }

        public IEnumerable<ISmartControl> FilterDevices(IEnumerable<ISmartControl> devices, DeviceFilter filter)
        {
            var result = new List<ISmartControl>();
            foreach (var d in devices)
            {
                if (filter(d)) result.Add(d);
            }
            return result;
        }
    }
}
