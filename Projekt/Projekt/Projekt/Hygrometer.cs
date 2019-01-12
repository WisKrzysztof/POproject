using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Hygrometer : Measurer
    {
        public Hygrometer() : base()
        {      }

        public Hygrometer(string s, double a) : base(s,a)
        { }

        public double Measure()
        {
            double p = location.Humidity - Acc/2 + rnd.NextDouble() * Acc;
            if (p > 1) return 1;
            if (p < 0) return 0;
            return Math.Round(p, 4);
           
        }


    }
}
