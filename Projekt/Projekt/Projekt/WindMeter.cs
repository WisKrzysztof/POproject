using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
   public class WindMeter : Measurer
    {


        int degAcc;

        public int DegAcc
        {
            get
            {
                return degAcc;
            }

            set
            {
                degAcc = value;
            }
        }
        public WindMeter() : base()
        { }
        public WindMeter(string s, double a, int b) : base(s,a)
        { DegAcc = b; }

        public Wind Measure()
        {
            Wind tmp;

            double p = location.Winds.strength + Acc / 2 + rnd.NextDouble() * Acc;
            double q = location.Winds.direction + DegAcc / 2 + rnd.NextDouble() * DegAcc;
            if (q < 0) q = q + 360;
            if (q > 360) q = q - 360;
            tmp.strength = Math.Round(p, 4);
            tmp.direction = Math.Round(q, 2);

            return tmp;
        }
    }
}
