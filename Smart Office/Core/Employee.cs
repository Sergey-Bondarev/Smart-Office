using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart_Office.Abstract;

namespace Smart_Office.Core
{
    public delegate double BonusCalculator(int accessLevel, double baseSalary);

    public class Employee : IComparable<Employee>, ICloneable
    {
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public double Salary { get; set; }

        public Employee(string name, int accessLevel, double salary)
        {
            Name = name;
            AccessLevel = accessLevel;
            Salary = salary;
        }

        public void ControlDevice(ISmartControl device)
        {
            if (AccessLevel > 2)
            {
                device.ExecuteDiagnostic();
            }
        }

        public double GetTotalIncome(BonusCalculator calc)
        {
            return Salary + calc(AccessLevel, Salary);
        }

        public int CompareTo(Employee other)
        {
            if (other == null) return 1;
            return AccessLevel.CompareTo(other.AccessLevel);
        }

        public object Clone()
        {
            return new Employee(this.Name, this.AccessLevel, this.Salary);
        }
    }
}
