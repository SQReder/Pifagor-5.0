using System;

namespace Pifagor.Geometry
{
    public static class GeometryExtensions
    {
        /// <summary>
        /// Позволяет получить угол между осью абсцисс и вектором
        /// </summary>
        /// <param name="vector">Вектор</param>
        /// <returns>Угол между осью абсцисс и вектором</returns>
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

        /// <summary>
        /// Позволяет получить нормаль вектора
        /// </summary>
        /// <param name="vector">Вектор для которого берется нормаль</param>
        /// <returns>Нормаль переданного вектора</returns>
        public static Vector Unit(this Vector vector)
        {
            var l = vector.Length;
            return Utils.IsEquals(l, 0) ? Vector.Zero : new Vector(vector.X / l, vector.Y / l);
        }

        /// <summary>
        /// Позволяет получить матрицу преобразования вектора (1,0) в указанный вектор
        /// </summary>
        /// <param name="b">Вектор</param>
        /// <returns>Матрица преобразования вектора (1 0) в указанный</returns>
        /// <seealso cref="GetBaseVectorTransformation(double, double)"/>
        public static TransformationMatrix GetBaseVectorTransformation(this Vector b)
        {
            return GetBaseVectorTransformation(b.X, b.Y);
        }

        /// <summary>
        /// Позволяет получить матрицу преобразования вектора (1,0) в вектор к указанной точке
        /// </summary>
        /// <param name="x">Координата по горизонтали</param>
        /// <param name="y">Координата по вертикали</param>
        /// <returns>Матрица преобразования вектора (1 0) в вектор к указанной точке</returns>
        public static TransformationMatrix GetBaseVectorTransformation(double x, double y)
        {
            var vector = new Vector(x, y);

            var rotate = new RotationMatrix(vector.Angle());
            var scale = new ScaleMatrix(vector.Length);

            return rotate*scale;
        }
    }
}