using System;
using System.Globalization;

namespace Pifagor.Geometry
{
    public static class Utils
    {
        internal const double AbsTol = 0.000000001;
        internal const double RelTol = 0.000001;

        public static bool IsEquals(double x, double y)
        {
            return (Math.Abs(x - y) <= Math.Max(AbsTol, RelTol * Math.Max(Math.Abs(x), Math.Abs(y))));
        }

        public static string Format(double d)
        {
            return Math.Round(d, 9).ToString(CultureInfo.CurrentCulture);
        }
    }
}