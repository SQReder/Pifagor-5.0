using System;
using System.Globalization;

namespace Pifagor.Geometry
{
    public static class Utils
    {
        internal const double AbsTol = 0.000000001;
        internal const double RelTol = 0.00000000000001;

        /// <summary>
        /// ���������� ��� ����� � ��������� ������ � ������ ������������ �������������
        /// </summary>
        /// <param name="x">������ �����</param>
        /// <param name="y">������ �����</param>
        /// <returns>������, ���� ���������� ����� ����� (� ��������� �� �������), ����� ����</returns>
        public static bool IsEquals(double x, double y)
        {
            return (Math.Abs(x - y) <= Math.Max(AbsTol, RelTol * Math.Max(Math.Abs(x), Math.Abs(y))));
        }

        /// <summary>
        /// ��������� ����� �� ���������� ������ � ����������� � ������
        /// </summary>
        /// <param name="d">����� ��� ����������</param>
        /// <returns>������ � ����������� ������ ������</returns>
        public static string Format(double d)
        {
            return Math.Round(d, 9).ToString(CultureInfo.CurrentCulture);
        }
    }
}