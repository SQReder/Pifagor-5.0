using System;
using System.Diagnostics.CodeAnalysis;

namespace Pifagor.Geometry
{
    public class Vector: TransformationMatrix
    {
        #region Properties

        public double X
        {
            get { return this[2,0]; }
            set { this[2, 0] = value; }
        }

        public double Y
        {
            get { return this[2,1]; }
            set { this[2, 1] = value; }
        }

        #endregion

        #region Contructors

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public members

        public double Length => Math.Sqrt(X*X + Y*Y);
        public static Vector Zero => new Vector(0,0);

        #endregion

        #region Operator overloading

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, b.X - b.Y);
        }

        public static Vector operator *(Vector a, double k)
        {
            return new Vector(a.X*k, a.Y*k);
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.X*b.X + a.Y*b.Y;
        }

        #endregion

        #region Equality members

        [ExcludeFromCodeCoverage]
        protected bool Equals(Vector other)
        {
            return Compare.IsEquals(X,other.X) && Compare.IsEquals(Y, other.Y);
        }

        [ExcludeFromCodeCoverage]
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
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }

        #endregion

        #region Formatting members 

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"{base.ToString()}, X: {X}, Y: {Y}";
        }

        #endregion
    }
}
