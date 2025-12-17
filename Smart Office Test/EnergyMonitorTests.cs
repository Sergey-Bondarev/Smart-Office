using Smart_Office.Abstract;
using Smart_Office.Core;

namespace Smart_Office_Test;

[TestClass]
public class EnergyMonitorTests
{
    private EnergyMonitor _monitor;
    private List<IPowerConsuming> _consumers;
    private List<ISmartControl> _allDevices;

    [TestInitialize]
    public void Setup()
    {
        _monitor = new EnergyMonitor();

        var light = new LightSystem("L-001", "LED");
        light.PerformAction();
        var climate = new ClimateControl("C-002", "AC");

        _consumers = new List<IPowerConsuming> { light, climate };

        _allDevices = new List<ISmartControl> { light, climate, new MockControlPanel() };
    }

    [TestMethod]
    public void CalculateTotalConsumption_ShouldSumUsageFromDifferentTypes()
    {
        double expectedTotal = 200.0;
        double actualTotal = _monitor.CalculateTotalConsumption(_consumers);

        Assert.AreEqual(expectedTotal, actualTotal, 0.001, "General usage should be calculated by interface");
    }

    [TestMethod]
    public void FilterDevices_ShouldReturnOnlyOnlineDevices()
    {
        DeviceFilter onlineFilter = (device) => device.IsOnline;

        var light = (LightSystem)_consumers.First(c => c is LightSystem);
        light.IsOnline = false;

        var filteredList = _monitor.FilterDevices(_allDevices.Cast<ISmartControl>().ToList(), onlineFilter);

        Assert.AreEqual(2, filteredList.Count(), "Only active devices should be returned");
        Assert.IsTrue(filteredList.All(d => d.IsOnline), "All filtered devices should be active");
    }
}
