using System;

namespace Pifagor.Geometry
{
    public static class GeometryExtensions
    {

        public static Vector ToVector(this RadialVector radialVector)
        {
            var x = radialVector.r * Math.Cos(radialVector.a);
            var y = radialVector.r * Math.Sin(radialVector.a);
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
            return Compare.IsEquals(l, 0) ? new Vector(0, 0) : new Vector(vector.x / l, vector.y / l);
        }
    }
}