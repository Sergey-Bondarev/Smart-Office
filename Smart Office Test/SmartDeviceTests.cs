using Smart_Office.Abstract;
using Smart_Office.Core;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Office_Test;

[TestClass]
public class SmartDeviceTests
{
    [TestMethod]
    public void PerformAction_LightSystem_ShouldSetBrightnessTo100()
    {
        var light = new LightSystem("L-101", "Philips");

        light.PerformAction();

        Assert.AreEqual(100, light.Brightness, "PerformAction should set light to 100.");
    }

    [TestMethod]
    public void PerformAction_ClimateControl_ShouldSetTargetTemperatureTo22_5()
    {
        var climate = new ClimateControl("C-202", "Model 1");

        climate.PerformAction();

        Assert.AreEqual(22.5, climate.TargetTemperature, "PerformAction should set temperature to 22.5.");
    }

    [TestMethod]
    public void ExecuteDiagnostic_ShouldTriggerOnStateChangedEvent()
    {
        var light = new LightSystem("L-101", "Philips");
        string capturedStatus = null;

        light.OnStateChanged += (id, status) =>
        {
            capturedStatus = status;
        };

        light.ExecuteDiagnostic();

        Assert.IsNotNull(capturedStatus, "Event OnStateChanged should be called");
        StringAssert.Contains(capturedStatus, "Working", "Stutus should consist info");
    }

    [TestMethod]
    public void PowerConsuming_ShouldCalculateCorrectUsage()
    {
        var light = new LightSystem("L-101", "Philips");
        var climate = new ClimateControl("C-202", "Model 1");

        IPowerConsuming consumer1 = light;
        IPowerConsuming consumer2 = climate;

        light.PerformAction();

        double usage1 = consumer1.CurrentPowerUsage;
        double usage2 = consumer2.CurrentPowerUsage;

        Assert.AreEqual(50.0, usage1, 0.001, "LightSystem should be used for 50.0");
        Assert.AreEqual(150.0, usage2, 0.001, "ClimateControl should be used for 150.0");

        Assert.AreEqual(50.0 * 24, light.GetDailyReport(), "DailyReport LightSystem incorrect");
    }
}
