using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ThermometerTest
    {
        [TestMethod]
        public void TestMeasuringTemperature()
        {
            //  given
            Projekt.Thermometer thermometer = new Projekt.Thermometer("t1", 2.0);
            Projekt.Location location = new Projekt.Location(30, 40, 300);
            location.Temperature = 25;
            thermometer.location = location;

            // when
            double temperature = thermometer.Measure();

            // then
            Assert.IsTrue(temperature > 24);
            Assert.IsTrue(temperature < 26);
        }

        [TestMethod]
        public void TestSettingName()
        {
            Projekt.Thermometer thermometer = new Projekt.Thermometer("t2", 3.0);

            string name = thermometer.Name;

            Assert.AreEqual(name, "t2");
        }
    }
}
