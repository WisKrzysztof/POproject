using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Thermometer : Measurer, IMeasurable
    {



        public Thermometer() : base()
        { }
        public Thermometer(string s, double a) : base(s,a)
        {       }



        public double Measure()
        {
            
            return Math.Round(location.Temperature - Acc / 2 + rnd.NextDouble() * Acc, 4);
        }
    }
}
