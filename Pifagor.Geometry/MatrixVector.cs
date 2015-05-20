using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pifagor.Geometry
{
    class MatrixVector
    {
        #region Fields

        private readonly double[,] _data = new double[3, 3];

        #endregion


        #region Constructors

        public MatrixVector()
        {
            this[2, 2] = 1;
        }

        public MatrixVector(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }

        public MatrixVector(double x, double y, double dx, double dy)
            : this()
        {
            X = x;
            Y = y;
            DX = dx;
            DY = dy;
        }

        #endregion


        #region Public members

        public double this[int row, int col]
        {
            get { return _data[row, col]; }
            set { _data[row, col] = value; }
        }

        #region Quick accessors

        public double X
        {
            get { return _data[0, 0]; }
            set { _data[0, 0] = value; }
        }

        public double Y
        {
            get { return _data[1, 1]; }
            set { _data[1, 1] = value; }
        }

        public double DX
        {
            get { return _data[2, 0]; }
            set { _data[2, 0] = value; }
        }

        public double DY
        {
            get { return _data[2, 1]; }
            set { _data[2, 1] = value; }
        }


        #endregion Quick accessors

        #endregion Public members

        #region Operators overloading

        public static MatrixVector operator +(MatrixVector a, MatrixVector b)
        {
            return new MatrixVector(a.X + b.X, a.Y + b.Y, a.DX + b.DX, a.DY + b.DY);
        }

        #endregion

        protected bool Equals(MatrixVector other)
        {
            if (!Compare.IsEquals(_data[0, 0], other._data[0, 0])) return false;
            if (!Compare.IsEquals(_data[1, 1], other._data[1, 1])) return false;
            if (!Compare.IsEquals(_data[2, 0], other._data[2, 0])) return false;
            if (!Compare.IsEquals(_data[2, 1], other._data[2, 1])) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MatrixVector) obj);
        }

        public override int GetHashCode()
        {
            return _data?.GetHashCode() ?? 0;
        }

        public static bool operator ==(MatrixVector left, MatrixVector right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MatrixVector left, MatrixVector right)
        {
            return !Equals(left, right);
        }
    }
}
