using Smart_Office.Abstract;
using Smart_Office.Core;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Office_Test;

[TestClass]
public class InfrastructureTests
{
    private Room _testRoom;
    private Employee _testEmployee;
    private LightSystem _testLight;

    [TestInitialize]
    public void Setup()
    {
        _testRoom = new Room("Testing Lab");
        _testEmployee = new Employee("Test User", 5, 50000);
        _testLight = new LightSystem("L-T", "TestModel");
    }

    [TestMethod]
    public void AssignEmployee_ShouldAddEmployeeToRoom()
    {
        int initialCount = _testRoom.Employees.Count;

        _testRoom.AssignEmployee(_testEmployee);

        Assert.AreEqual(initialCount + 1, _testRoom.Employees.Count, "Employee should be added into office room");
        Assert.IsTrue(_testRoom.Employees.Contains(_testEmployee), "Office room should include added employee");
    }

    [TestMethod]
    public void Room_ShouldBeEnumerableForDevices()
    {
        var climate = new ClimateControl("C-T", "TestClimate");
        _testRoom.AddDevice(_testLight);
        _testRoom.AddDevice(climate);

        int deviceCount = 0;
        foreach (var device in _testRoom)
        {
            deviceCount++;
            Assert.IsInstanceOfType(device, typeof(SmartDevice), "All objects must be SmartDevice type");
        }

        Assert.AreEqual(2, deviceCount, "Room should return correct number of devices in it");
    }

    [TestMethod]
    public void Office_Initialization_ShouldContainRooms()
    {
        var office = new Office();

        Assert.IsTrue(office.Rooms.Count > 0, "Office should be create with rooms");

        Assert.IsNotNull(office.Rooms.FirstOrDefault(r => r.Name == "Main Hall"), "Main Hall should exist");
    }

    [TestMethod]
    public void TriggerEmergency_ShouldFireOnEmergencyAlarmEvent()
    {
        var office = new Office();
        bool eventFired = false;
        string capturedMessage = null;
        string expectedMessagePart = "FIRE ALERT";

        office.OnEmergencyAlarm += (message) =>
        {
            eventFired = true;
            capturedMessage = message;
        };

        office.TriggerEmergency(expectedMessagePart);

        Assert.IsTrue(eventFired, "Event OnEmergencyAlarm should happen");
        StringAssert.Contains(capturedMessage, expectedMessagePart, "Event messages should be equal");
    }

}
