using System;

namespace Pifagor.Geometry
{
    public static class GeometryExtensions
    {

        public static Vector ToVector(this RadialVector radialVector)
        {
            var x = radialVector.R * Math.Cos(radialVector.A);
            var y = radialVector.R * Math.Sin(radialVector.A);
            return new Vector(x, y);
        }

        public static RadialVector ToRadialVector(this Vector vector)
        {
            var r = vector.Length;
            var a = Angle(vector);
            return new RadialVector(r, a);
        }

        public static double Angle(this Vector vector)
        {
            double a;
            if (Utils.IsEquals(vector.X, 0))
            {
                if (Utils.IsEquals(vector.Y, 0))
                {
                    a = 0;
                }
                else
                {
                    a = Math.PI/2d*Math.Sign(vector.Y);
                }
            }
            else
            {
                a = Math.Atan(vector.Y/vector.X) + ((vector.X < 0) ? Math.PI : 0);
            }
            return a;
        }

        public static Vector Unit(this Vector vector)
        {
            var l = vector.Length;
            return Utils.IsEquals(l, 0) ? Vector.Zero : new Vector(vector.X / l, vector.Y / l);
        }

        public static RadialVector Unit(this RadialVector r)
        {
            return Utils.IsEquals(0, r.R) ? RadialVector.Zero : new RadialVector(1, r.A);
        }
    }
}