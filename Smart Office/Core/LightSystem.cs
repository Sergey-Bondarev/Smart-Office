using Smart_Office.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Core
{
    public class LightSystem : SmartDevice, IPowerConsuming
    {
        public int Brightness { get; private set; }
        public double CurrentPowerUsage => IsOnline ? (Brightness * 0.5) : 0;

        public LightSystem(string id, string model) : base(id, model) { }

        public override void PerformAction()
        {
            Brightness = 100;
        }

        public double GetDailyReport() => CurrentPowerUsage * 24;
    }
}
