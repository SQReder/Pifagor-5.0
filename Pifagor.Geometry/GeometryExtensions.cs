﻿using System;

namespace Pifagor.Geometry
{
    public static class GeometryExtensions
    {
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

        public static TransformationMatrix GetBaseVectorTransformation(this Vector b)
        {
            return GetBaseVectorTransformation(b.X, b.Y);
        }

        public static TransformationMatrix GetBaseVectorTransformation(double x, double y)
        {
            var vector = new Vector(x, y);

            var rotate = new RotationMatrix(vector.Angle());
            var scale = new ScaleMatrix(vector.Length);

            return rotate*scale;
        }
    }
}