using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pifagor.Geometry
{
    public class Vector
    {
        #region Fields

        public double x;
        public double y;
        private const double Tolerance = 0.000001;

        #endregion

        #region Contructors

        public Vector() : this(0, 0)
        {
        }

        public Vector(double x, double y)
        {
            Set(x, y);
        }

        #endregion

        #region Public members

        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

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

        public static double operator ^(Vector a, Vector b)
        {
            return a.x*b.y - a.y*b.x;
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
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector) obj);
        }

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
