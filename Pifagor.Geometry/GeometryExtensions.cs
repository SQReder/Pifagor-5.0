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
            double a;
            if (Compare.IsEquals(vector.x, 0))
            {
                if (Compare.IsEquals(vector.y, 0))
                {
                    a = 0;
                }
                else
                {
                    a = Math.PI / 2d * Math.Sign(vector.y);
                }
                
            }
            else
            {
                a = Math.Atan(vector.y / vector.x) + ((vector.x < 0) ? Math.PI : 0);
                
            }
            return new RadialVector(r, a);
        }

        public static Vector Unit(this Vector vector)
        {
            var l = vector.Length;
            return Compare.IsEquals(l, 0) ? Vector.Zero : new Vector(vector.x / l, vector.y / l);
        }

        public static RadialVector Unit(this RadialVector r)
        {
            return Compare.IsEquals(0, r.R) ? RadialVector.Zero : new RadialVector(1, r.A);
        }
    }
}