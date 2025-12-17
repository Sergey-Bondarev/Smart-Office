using Smart_Office.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Office.Core
{
    public class Room : IEnumerable<SmartDevice>
    {
        public string Name { get; }

        public List<Employee> Employees { get; } = new List<Employee>();

        private List<SmartDevice> _devices = new List<SmartDevice>();

        public Room(string name) => Name = name;

        public void AddDevice(SmartDevice device) => _devices.Add(device);

        public void AssignEmployee(Employee emp) => Employees.Add(emp);

        public IEnumerator<SmartDevice> GetEnumerator() => _devices.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
