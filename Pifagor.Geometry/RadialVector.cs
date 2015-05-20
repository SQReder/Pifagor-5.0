using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pifagor.Geometry
{
    public struct RadialVector
    {
        public double r;
        public double a;

        public RadialVector(double r, double a)
        {
            this.r = r;
            this.a = a;
        }
    }
}
