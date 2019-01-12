using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projekt
{
    [Serializable]

    public struct Wind
    {
        public double strength;
        public double direction;



        public Wind(double a, double b)
        {
            strength = a;
            direction = b;
        }

        public override string ToString()
        {
            return strength.ToString() + "km/h " + direction.ToString() + "°";
        }

    }

    public struct Weather
    {
        public double temperature;
        public double humidity;
        public Wind winds;
        public double pressure;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Temperature: " + temperature + "°C");
            sb.AppendLine("Humidity: " + humidity + "%");
            sb.AppendLine("Pressure: " + pressure + " hPa");
            sb.AppendLine("Wind: " + winds.ToString());
            return sb.ToString();
        }
    }

    //The weather station has a slot for a Barometer, Hygrometer, Thermometer and WindMeter.
    //It is empty at the beginning, and it's impossible to put in two Measurers of the same type.

    public class WeatherStation : ICloneable, IComparable<WeatherStation>
    {

        //We are not using List<Measurer> for the list of instruments.
        //Checking whether instrument of said type is in the Weather Station would be complicated then.
        //That is why every instrument has its own variable, so that it can easily be substituted or checked if it's null.
        [Key]
        public int WeatherStationID { get; set; }
        public virtual StationList StationListBaza { get; set; }

        public Location loc;
        public string name;
        public Thermometer thermo;
        public WindMeter anemo;
        public Barometer bar;
        public Hygrometer hygro;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Thermometer Thermo
        {
            get
            {
                return thermo;
            }

            set
            {
                thermo = value;
            }
        }

        public WindMeter Anemo
        {
            get
            {
                return anemo;
            }

            set
            {
                anemo = value;
            }
        }

        public Barometer Bar
        {
            get
            {
                return bar;
            }

            set
            {
                bar = value;
            }
        }

        public Hygrometer Hygro
        {
            get
            {
                return hygro;
            }

            set
            {
                hygro = value;
            }
        }

        //This compares the locations of the Weather Stations.
        //From the lowest to highest, then from south to north, then from the west to east.
        public int CompareTo(WeatherStation w) 
        {
            if (loc == w.loc) return 0;
            if (loc.Altitude != w.loc.Altitude) return loc.Altitude.CompareTo(w.loc.Altitude);
            if (loc.Latitude != w.loc.Latitude) return loc.Latitude.CompareTo(w.loc.Latitude);
            return loc.Longitude.CompareTo(w.loc.Longitude);
        }
        public WeatherStation()
        {
            Name = "";
            loc = null;
            Thermo = null;
            Anemo = null;
            Bar = null;
            Hygro = null;

        }

        public WeatherStation(string s, Location l)
        {
            Name = s;
            loc = l;
            Thermo = null;
            Anemo = null;
            Bar = null;
            Hygro = null;

        }

        public object Clone()
        {
            WeatherStation cl = new WeatherStation(Name, loc);
            if (Thermo!=null) cl.Insert((Thermometer)Thermo.Clone());
            if (Anemo != null) cl.Insert((WindMeter)Anemo.Clone());
            if (Bar != null) cl.Insert((Barometer)Bar.Clone());
            if (Hygro != null) cl.Insert((Hygrometer)Hygro.Clone());

            return cl;
        }


        public void Insert(Measurer m)
        {
            Type tp = m.GetType();
            if (tp == typeof(Thermometer)) {
                if (Thermo != null) Thermo.location = null;
                Thermo = (Thermometer)m; m.location = loc;
            }
            if (tp == typeof(Barometer)) {
                if (Bar != null) Bar.location = null;
                Bar = (Barometer)m; m.location = loc;
            }
            if (tp == typeof(WindMeter)) {
                if (Anemo != null) Anemo.location = null;
                Anemo = (WindMeter)m; m.location = loc;
            }
            if (tp == typeof(Hygrometer)) {
                if (Hygro != null) Hygro.location = null;
                Hygro = (Hygrometer)m; m.location = loc;
            }

        }

        public double getTemperature()
        {
            if (Thermo == null) return Double.NaN;
            return Thermo.Measure();
        }

        public double getPressure()
        {
            if (Bar == null) return Double.NaN;
            return Bar.Measure();
        }
        public double getHumidity()
        {
            if (Hygro == null) return Double.NaN;
            return Hygro.Measure();
        }
        public Wind getWind()
        {
            if (Anemo == null) return new Wind(Double.NaN, Double.NaN);
            return Anemo.Measure();
        }

        public Weather MeasureAll()
        {
            Weather tmp = new Weather(); ;
            tmp.temperature = getTemperature();
            tmp.pressure = getPressure();
            tmp.humidity = getHumidity();
            tmp.winds = getWind();
            
            return tmp;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(name);
            sb.AppendLine("Modules: ");
            if (Thermo != null) { sb.Append("Thermometer: "); sb.AppendLine(Thermo.Name); }
            if (Anemo != null) { sb.Append("Wind Meter: "); sb.AppendLine(Anemo.Name); }
            if (Bar != null) { sb.Append("Barometer: "); sb.AppendLine(Bar.Name); }
            if (Hygro != null) { sb.Append("Hygrometer: "); sb.AppendLine(Hygro.Name); }



            return sb.ToString();
        }


    }
}
