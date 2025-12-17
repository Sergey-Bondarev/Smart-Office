using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Abstract
{
    public delegate void DeviceStateChangedHandler(string deviceId, string status);

    public abstract class SmartDevice : ISmartControl
    {
        public string DeviceId { get; protected set; }
        public string ModelName { get; set; }
        public bool IsOnline { get; set; }

        public event DeviceStateChangedHandler OnStateChanged;

        protected SmartDevice(string id, string model)
        {
            DeviceId = id;
            ModelName = model;
            IsOnline = true;
        }

        public abstract void PerformAction();

        public virtual void ExecuteDiagnostic()
        {
            string status = IsOnline ? "Working" : "Offline";
            OnStateChanged?.Invoke(DeviceId, $"Diagnostic run: {status}");
        }
    }
}
