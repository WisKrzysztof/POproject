using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projekt;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class StationListTest
    {
        [TestMethod]
        public void Sorting()
        {
            StationList stationList = new StationList("s");
            WeatherStation weatherStation1 = new WeatherStation("s1", new Location(50, 50, 100 ));
            WeatherStation weatherStation2 = new WeatherStation("s1", new Location(50, 50, 70));
            WeatherStation weatherStation3 = new WeatherStation("s1", new Location(25, 25, 70));
            WeatherStation weatherStation4 = new WeatherStation("s1", new Location(25, 50, 70));

            stationList.Add(weatherStation1);
            stationList.Add(weatherStation2);
            stationList.Add(weatherStation3);
            stationList.Add(weatherStation4);

            stationList.Sortuj();

            List<WeatherStation> stationsAfterSorting = stationList.list;

            Console.WriteLine(stationsAfterSorting);
            CollectionAssert.AreEqual(new List<WeatherStation> { weatherStation3, weatherStation4, weatherStation2, weatherStation1 }, stationsAfterSorting);

        }
    }
}
