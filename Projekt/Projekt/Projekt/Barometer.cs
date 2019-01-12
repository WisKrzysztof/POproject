using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Barometer : Measurer, IMeasurable
    {
        public Barometer() : base()
        { }
        public Barometer(string s, double a) : base(s, a)
        { }

        public double Measure()
        {
            double p = location.Pressure - Acc / 2 + rnd.NextDouble() * Acc;
            if (p < 0) return 0;      
            return Math.Round(p,4);
        }


    }
}
