using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{[Serializable]
    public class Location
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        private double latitude;  //N-S
        private double longitude; //W-E
        private int altitude;     //In meters
        private string city;      //name of city

        //These are hidden, and not really in actual database. This is to simulate the actual weather, and is generated randomly upon creation of location.
        private double temperature;  // in Celcius
        private double humidity;     // in percent
        private Wind winds;
        //private double windStrength; // in km/h
        //private double windDirection;   // in Degress, starting with N and going clockwise 
        private double pressure;     // hPa
        
        public void SimulateTimePassing()
        {
            
            temperature = temperature + rnd.NextDouble() * 10 - 5;          
            humidity = humidity + rnd.NextDouble() - 0.5;
            humidity = 0.4;
            if (humidity > 1) humidity = 1;
            if (humidity < 0) humidity = 0;
            winds.strength = winds.strength + rnd.NextDouble() * 10 - 5;
            if (winds.strength < 0) winds.strength = 0;
            winds.direction = winds.direction + rnd.NextDouble() * 20 - 10;
            if (winds.direction < 0) winds.direction = winds.direction + 360;
            if (winds.direction > 360) winds.direction = winds.direction - 360;
            pressure = pressure + rnd.NextDouble() * 6 - 3;
            if (pressure < 0) pressure = 0;
        }
 
        

        public double Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value;
            }
        }

        public int Altitude
        {
            get
            {
                return altitude;
            }

            set
            {
                altitude = value;
            }
        }

        public string City
        {
            get
            { return city; }
            set
            { city = value; }
        }

        public double Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                temperature = value;
            }
        }

        public double Humidity
        {
            get
            {
                return humidity;
            }

            set
            {
                humidity = value;
            }
        }

      

        public double Pressure
        {
            get
            {
                return pressure;
            }

            set
            {
                pressure = value;
            }
        }

        public Wind Winds
        {
            get
            {
                return winds;
            }

            set
            {
                winds = value;
            }
        }
         
        
        public Location()
        {            
            Latitude = 0;
            Longitude = 0;
            Altitude = 0;
            City = "";
    }

        public Location(double a, double b, int x, string n)
        {
            City = n;
            Latitude = a;
            Longitude = b;
            Altitude = x;

            Temperature = rnd.NextDouble() + rnd.Next() % 30 + 5;
            Humidity = rnd.NextDouble();           
            winds.strength = rnd.NextDouble() + rnd.Next() % 100;
            winds.direction = rnd.NextDouble() + rnd.Next() % 360;
            Pressure = rnd.Next() % 80 + 960;
            

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Lokalizacja stacji: "+ City + " "+ "\n");
            sb.Append(Math.Abs(Latitude));
            if (Latitude >= 0) sb.AppendLine("°N");
            else sb.AppendLine("°S");
            sb.Append(Math.Abs(Longitude));
            if (Longitude >= 0) sb.AppendLine("°E");
            else sb.AppendLine("°W");
            sb.Append("Altitude: ");
            sb.Append(Altitude.ToString());
            sb.AppendLine("m");

            return sb.ToString();
        }

    }
}
