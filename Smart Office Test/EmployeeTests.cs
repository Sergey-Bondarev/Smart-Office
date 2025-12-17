using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smart_Office.Abstract;
using Smart_Office.Core;
using System.Collections.Generic;
using System.Linq;

public class MockControlPanel : ISmartControl
{
    public string DeviceId { get; } = "MOCK-001";
    public bool IsOnline { get; } = true;
    public bool DiagnosticExecuted { get; private set; } = false;

    public void ExecuteDiagnostic()
    {
        DiagnosticExecuted = true;
    }
}


[TestClass]
public class EmployeeTests
{
    [TestMethod]
    public void Clone_ShouldCreateIndependentCopy()
    {
        var original = new Employee("Alex B", 5, 80000);

        var clone = (Employee)original.Clone();

        Assert.AreNotSame(original, clone, "The Clone should be the seperate object");
        Assert.AreEqual(original.Name, clone.Name, "Name is not matching");

        clone.Salary = 100000;
        Assert.AreNotEqual(original.Salary, clone.Salary, "The Clone changes should not effect the Original");
    }

    [TestMethod]
    public void CompareTo_ShouldSortByAccessLevel()
    {
        var employee1 = new Employee("Bob", 1, 30000);
        var employee2 = new Employee("Alice", 5, 90000);
        var employee3 = new Employee("Charlie", 3, 50000);

        var list = new List<Employee> { employee2, employee1, employee3 };

        list.Sort();

        Assert.AreEqual("Bob", list[0].Name, "Wrong Access level, 1 expected");
        Assert.AreEqual("Charlie", list[1].Name, "Wrong Access level, 3 expected");
        Assert.AreEqual("Alice", list[2].Name, "Wrong Access level, 5 expected");
    }

    [TestMethod]
    public void GetTotalIncome_ShouldUseCustomBonusCalculation()
    {
        var employee = new Employee("High Performer", 4, 60000);

        BonusCalculator specialBonus = (level, salary) =>
        {
            return level >= 4 ? salary * 0.20 : 0;
        };

        double expectedIncome = 72000;

        double actualIncome = employee.GetTotalIncome(specialBonus);

        Assert.AreEqual(expectedIncome, actualIncome, "Salary with bonus incorrect");
    }

    [TestMethod]
    public void ControlDevice_HighAccessLevel_ShouldExecuteDiagnostic()
    {
        var highLevelEmployee = new Employee("Manager", 4, 80000);
        var mockDevice = new MockControlPanel();

        highLevelEmployee.ControlDevice(mockDevice);

        Assert.IsTrue(mockDevice.DiagnosticExecuted, "Wrong stuff access level for this operation");
    }

    [TestMethod]
    public void ControlDevice_LowAccessLevel_ShouldNotExecuteDiagnostic()
    {
        var lowLevelEmployee = new Employee("Intern", 2, 20000);
        var mockDevice = new MockControlPanel();

        lowLevelEmployee.ControlDevice(mockDevice);

        Assert.IsFalse(mockDevice.DiagnosticExecuted, "Wrong stuff access level for this operation");
    }
}
