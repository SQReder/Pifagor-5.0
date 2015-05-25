using System;
using System.Diagnostics.CodeAnalysis;

namespace Pifagor.Geometry
{
    public class Vector
    {
        #region Fields

        public readonly double x;
        public readonly double y;

        #endregion

        #region Contructors

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Public members

        public double Length => Math.Sqrt(x*x + y*y);
        public static Vector Zero => new Vector(0,0);

        #endregion

        #region Operator overloading

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, b.x - b.y);
        }

        public static Vector operator *(Vector a, double k)
        {
            return new Vector(a.x*k, a.y*k);
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.x*b.x + a.y*b.y;
        }

        #endregion

        #region Equality members

        protected bool Equals(Vector other)
        {
            return Compare.IsEquals(x,other.x) && Compare.IsEquals(y, other.y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vector) obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode()*397) ^ y.GetHashCode();
            }
        }

        #endregion

    }
}
