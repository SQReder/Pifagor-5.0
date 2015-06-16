using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Pifagor.Geometry
{
    public struct Vector
    {
        #region Contructors

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public members

        public static Vector Base => new Vector(1, 0);
        public static Vector Zero => new Vector(0, 0);

        #endregion

        #region IVector members

        public double Length => Math.Sqrt(X * X + Y * Y);

        public double X { get; private set; }
        public double Y { get; private set; }

        #endregion

        #region Operator overloading

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector a, double k)
        {
            return new Vector(a.X*k, a.Y*k);
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.X*b.X + a.Y*b.Y;
        }

        public static Vector operator *(Vector v, TransformationMatrix t)
        {
            var x = v.X;
            var y = v.Y;

            v.X = x * t[0, 0] + y * t[1, 0] + t[2, 0];
            v.Y = x * t[0, 1] + y * t[1, 1] + t[2, 1];

            return v;
        }
        #endregion

        #region Equality members

        [ExcludeFromCodeCoverage]
        public bool Equals(Vector other)
        {
            return Utils.IsEquals(X, other.X) && Utils.IsEquals(Y, other.Y);
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
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
            return $"({Utils.Format(X)}, {Utils.Format(Y)})";
        }

        #endregion

        public static implicit operator PointF(Vector v)
        {
            return new PointF((float) v.X, (float) v.Y);
        }
    }
}
