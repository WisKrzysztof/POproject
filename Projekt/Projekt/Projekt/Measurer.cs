using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public abstract class Measurer: ICloneable
    {
        public Random rnd = new Random(Guid.NewGuid().GetHashCode());
        private string name;
        public Location location;
        private double acc;
        // Even though location is a pointer, we don't want to create a new location,
        // because Measurer doesn't contain the location, it only points to it.
        // That's why shallow copy works here.
        public object Clone() 
        {
            return MemberwiseClone();
        }

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

        public double Acc
        {
            get
            {
                return acc;
            }

            set
            {
                acc = value;
            }
        }

        public Measurer()
        {
            Name = "";
            location = null;
            Acc = 0;
        }
        public Measurer(string s, double a)
        {
            Name = s;
            location = null;
            Acc = a;
        }


    }
}
