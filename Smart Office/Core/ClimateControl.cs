using Smart_Office.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Core
{
    public class ClimateControl : SmartDevice, IPowerConsuming
    {
        public double TargetTemperature { get; set; }

        public double CurrentPowerUsage => IsOnline ? 150.0 : 5.0;

        public ClimateControl(string id, string model) : base(id, model) { }

        public override void PerformAction()
        {
            TargetTemperature = 22.5;
        }

        public double GetDailyReport() => CurrentPowerUsage * 24;
    }
}
