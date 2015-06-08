using System;
using System.Diagnostics.CodeAnalysis;

namespace Pifagor.Geometry
{
    public class TransformationMatrix
    {
        protected readonly double[,] _data = new double[3, 3];

        public static TransformationMatrix Noop
            => new TransformationMatrix();

        protected TransformationMatrix()
        {
            for (var i = 0; i != 3; ++i)
                _data[i, i] = 1;
        }

        public double this[int row, int col]
        {
            get { return _data[row, col]; }
            set { _data[row, col] = value; }
        }

        public static TransformationMatrix operator *(TransformationMatrix left, TransformationMatrix right)
        {
            var matrix = new TransformationMatrix();
            for (var i = 0; i != 3; ++i)
                for (var j = 0; j != 3; ++j)
                {
                    double val = 0;
                    for (var v = 0; v < 3; v++)
                    {
                        val += left[i,v] * right[v,j];
                    }
                    matrix[i,j] = val;

                }
            return matrix;
        }

        #region Equaliry members

        [ExcludeFromCodeCoverage]
        protected bool Equals(TransformationMatrix other)
        {
            if (!Utils.IsEquals(_data[0, 0], other._data[0, 0])) return false;
            if (!Utils.IsEquals(_data[0, 1], other._data[0, 1])) return false;
            if (!Utils.IsEquals(_data[1, 0], other._data[1, 0])) return false;
            if (!Utils.IsEquals(_data[1, 1], other._data[1, 1])) return false;
            if (!Utils.IsEquals(_data[2, 0], other._data[2, 0])) return false;
            if (!Utils.IsEquals(_data[2, 1], other._data[2, 1])) return false;

            return true;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TransformationMatrix) (object) (TransformationMatrix) obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return _data?.GetHashCode() ?? 0;
        }

        [ExcludeFromCodeCoverage]
        public static bool operator ==(TransformationMatrix left, TransformationMatrix right)
        {
            return Equals(left, right);
        }

        [ExcludeFromCodeCoverage]
        public static bool operator !=(TransformationMatrix left, TransformationMatrix right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Formatting members

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return
                $"R0: ({Utils.Format(_data[0, 0])},{Utils.Format(_data[0, 1])}), R1: ({Utils.Format(_data[1, 0])},{Utils.Format(_data[1, 1])}), TX: {Utils.Format(_data[2, 0])}, TY: {Utils.Format(_data[2, 1])}";
        }

        #endregion
    }

    public class TranslationMatrix : TransformationMatrix
    {
        public double TX
        {
            get { return _data[2, 0]; }
            set { _data[2, 0] = value; }
        }

        public double TY
        {
            get { return _data[2, 1]; }
            set { _data[2, 1] = value; }
        }

        public TranslationMatrix(): this(1,1)
        { }

        public TranslationMatrix(double tx, double ty) : base()
        {
                TX = tx;
                TY = ty;
        }
    }

    public class RotationMatrix : TransformationMatrix
    {
        public RotationMatrix() : this(0)
        { }

        public RotationMatrix(double alpha)
        {
            RotationMatrixBySinCos(Math.Sin(alpha), Math.Cos(alpha));
        }

        private void RotationMatrixBySinCos(double sinalpha, double cosalpha)
        {
            _data[0, 0] = cosalpha;
            _data[0, 1] = sinalpha;
            _data[1, 0] = -sinalpha;
            _data[1, 1] = cosalpha;
        }
    }

    public class ScaleMatrix : TransformationMatrix
    {
        public ScaleMatrix(): this(1)
        { }

        public ScaleMatrix(double kx, double ky)
        {
                _data[0,0] = kx;
                _data[1,1] = ky;
        }

        public ScaleMatrix(double k):this(k,k)
        {}
    }
}
