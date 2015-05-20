using System;

namespace Pifagor.Geometry
{
    public static class Compare
    {
        internal const double AbsTol = 0.000000001;
        internal const double RelTol = 0.000001;

        public static bool IsEquals(double x, double y)
        {
            return (Math.Abs(x - y) <= Math.Max(AbsTol, RelTol * Math.Max(Math.Abs(x), Math.Abs(y))));
        }
    }
}