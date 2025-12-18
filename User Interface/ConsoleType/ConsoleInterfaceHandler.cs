using Smart_Office.Abstract;
using Smart_Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Interface.ConsoleType
{
    internal class ConsoleInterfaceHandler
    {
        private Office _office;
        private EnergyMonitor _energyMonitor;
        private List<ISmartControl> _allDevices;

        public ConsoleInterfaceHandler()
        {
            _energyMonitor = new EnergyMonitor();
            InitializeSystem();
        }

        private void InitializeSystem()
        {
            _office = new Office();

            var itDept = _office.Rooms.FirstOrDefault(r => r.Name == "IT Department");
            var mainHall = _office.Rooms.FirstOrDefault(r => r.Name == "Main Hall");

            if (itDept != null)
            {
                itDept.AddDevice(new LightSystem("L-101", "Philips Hue"));
                itDept.AddDevice(new ClimateControl("C-201", "Daikin Smart"));
                itDept.AssignEmployee(new Employee("Alice", 5, 90000));
                itDept.AssignEmployee(new Employee("Bob", 2, 45000));
            }

            _office.OnEmergencyAlarm += message =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[System Security]: {message}");
                Console.ResetColor();
            };

            _allDevices = new List<ISmartControl>();
            foreach (var room in _office.Rooms)
            {
                foreach (var device in room)
                {
                    _allDevices.Add(device);
                    ((SmartDevice)device).OnStateChanged += (id, status) =>
                        Console.WriteLine($"   > Current Log {id}: {status}");
                }
            }
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowOfficeStatus(); break;
                    case "2": SortAndShowEmployees(); break;
                    case "3": RunGlobalDiagnostics(); break;
                    case "4": ShowEnergyReport(); break;
                    case "5": TriggerEmergency(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Incorrect input"); break;
                }
                Console.WriteLine("\nPress any key for continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("=== Smart Office Control Panel ===");
            Console.WriteLine("1. Status of room and devices");
            Console.WriteLine("2. List of Employees (IComparable)");
            Console.WriteLine("3. Start general diagnostic (Polymorphism)");
            Console.WriteLine("4. Report of energy usage (Interfaces)");
            Console.WriteLine("5. Alarm (Events)");
            Console.WriteLine("0. Exit");
            Console.Write("\nTake a choice: ");
        }

        private void ShowOfficeStatus()
        {
            Console.WriteLine("--- Office Status ---");
            foreach (var room in _office.Rooms)
            {
                Console.WriteLine($"Room: {room.Name}");
                foreach (var device in room)
                {
                    Console.WriteLine($"  - [{device.DeviceId}] {device.GetType().Name} | Online: {device.IsOnline}");
                }
            }
        }

        private void SortAndShowEmployees()
        {
            Console.WriteLine("--- Employees (sprted by access level with IComparable) ---");
            var allEmps = _office.Rooms.SelectMany(r => r.Employees).ToList();

            allEmps.Sort();

            foreach (var emp in allEmps)
            {
                Console.WriteLine($"[{emp.AccessLevel}] {emp.Name} - {emp.Salary:C}");
            }
        }

        private void RunGlobalDiagnostics()
        {
            Console.WriteLine("--- Diagnostic start up ---");
            foreach (var device in _allDevices)
            {
                device.ExecuteDiagnostic();
            }
        }

        private void ShowEnergyReport()
        {
            Console.WriteLine("--- Energy Usage ---");
            var consumers = _allDevices.OfType<IPowerConsuming>();
            double total = _energyMonitor.CalculateTotalConsumption(consumers);

            Console.WriteLine($"Current office usage: {total} Вт");
        }

        private void TriggerEmergency()
        {
            _office.TriggerEmergency("ALARM! SMOKE LOCATED IN SERVER ROOM");
        }
    }
}
