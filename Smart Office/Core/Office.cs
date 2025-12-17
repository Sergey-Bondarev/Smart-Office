using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Core
{
    public class Office
    {
        public List<Room> Rooms { get; private set; }

        public event Action<string> OnEmergencyAlarm;

        public Office()
        {
            Rooms = new List<Room>
            {
                new Room("Main Hall"),
                new Room("IT Department")
            };
        }

        public void TriggerEmergency(string message)
        {
            OnEmergencyAlarm?.Invoke($"EMERGENCY in Office: {message}");
        }
    }
}
