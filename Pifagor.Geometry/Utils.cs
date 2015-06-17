using System;
using System.Globalization;

namespace Pifagor.Geometry
{
    public static class Utils
    {
        internal const double AbsTol = 0.000000001;
        internal const double RelTol = 0.00000000000001;

        /// <summary>
        /// Сравнивает два числа с плавающей точкой с учетом особенностей представления
        /// </summary>
        /// <param name="x">Первое число</param>
        /// <param name="y">Второе число</param>
        /// <returns>Истина, ести переданные числа равны (с точностью до эпсилон), иначе Ложь</returns>
        public static bool IsEquals(double x, double y)
        {
            return (Math.Abs(x - y) <= Math.Max(AbsTol, RelTol * Math.Max(Math.Abs(x), Math.Abs(y))));
        }

        /// <summary>
        /// Округляет число до ближайшего целого и преобразует в строку
        /// </summary>
        /// <param name="d">Число для округления</param>
        /// <returns>Строку с округленным числом числом</returns>
        public static string Format(double d)
        {
            return Math.Round(d, 9).ToString(CultureInfo.CurrentCulture);
        }
    }
}