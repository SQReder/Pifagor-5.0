﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Pifagor.Geometry
{
    class TransformationMatrix
    {
        #region Fields

        private readonly double[,] _data = new double[3, 3];

        #endregion


        #region Constructors

        protected TransformationMatrix()
        {
            for (var i = 0; i != 3; ++i)
                _data[i, i] = 1;
        }

        #endregion

        #region Fabric methods

        public static TransformationMatrix TranslationMatrix(double tx, double ty)
        {
            return new TransformationMatrix
            {
                [2, 0] = tx,
                [2, 1] = ty
            };
        }

        public static TransformationMatrix RotationMatrix(double alpha)
        {
            return RotationMatrixBySinCos(Math.Sin(alpha), Math.Cos(alpha));
        }

        private static TransformationMatrix RotationMatrixBySinCos(double sinalpha, double cosalpha)
        {
            return new TransformationMatrix
            {
                [0, 0] = cosalpha,
                [0, 1] = sinalpha,
                [1, 0] = -sinalpha,
                [1, 1] = cosalpha
            };
        }

        #endregion

        #region Public members

        private double this[int row, int col]
        {
            get { return _data[row, col]; }
            set { _data[row, col] = value; }
        }

        #endregion Public members

        #region Operators overloading

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

        public static Vector operator *(Vector v, TransformationMatrix t)
        {
            var x = v.x;
            var y = v.y;
            return new Vector(
                x * t[0, 0] + y * t[1, 0] + t[2, 0],
                x * t[0, 1] + y * t[1, 1] + t[2, 1]
            );
        }

        #endregion

        #region Equality members

        protected bool Equals(TransformationMatrix other)
        {
            if (!Compare.IsEquals(_data[0, 0], other._data[0, 0])) return false;
            if (!Compare.IsEquals(_data[0, 1], other._data[0, 1])) return false;
            if (!Compare.IsEquals(_data[1, 0], other._data[1, 0])) return false;
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
            return Equals((TransformationMatrix) obj);
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
            return $"R0: ({_data[0, 0]},{_data[0, 1]}), R1: ({_data[1, 0]},{_data[1, 1]}), TX: {_data[2, 0]}, TY: {_data[2, 1]}";
        }

        #endregion
    }
}
