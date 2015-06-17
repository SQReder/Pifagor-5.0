using System;
using System.Diagnostics.CodeAnalysis;

namespace Pifagor.Geometry
{
    /// <summary>
    /// Представляет матрицу трансформации в общем виде
    /// </summary>
    public class TransformationMatrix
    {
        protected readonly double[,] Data = new double[3, 3];

        protected TransformationMatrix()
        {
            for (var i = 0; i != 3; ++i)
                Data[i, i] = 1;
        }

        public double this[int row, int col]
        {
            get { return Data[row, col]; }
            private set { Data[row, col] = value; }
        }

        /// <summary>
        /// Перемножает две матрицы трансформации
        /// </summary>
        /// <param name="left">Левая матрица</param>
        /// <param name="right">Правая матрица</param>
        /// <returns>Результат умножения матриц</returns>
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

        #region Equality members

        [ExcludeFromCodeCoverage]
        public bool Equals(TransformationMatrix other)
        {
            if (!Utils.IsEquals(Data[0, 0], other.Data[0, 0])) return false;
            if (!Utils.IsEquals(Data[0, 1], other.Data[0, 1])) return false;
            if (!Utils.IsEquals(Data[1, 0], other.Data[1, 0])) return false;
            if (!Utils.IsEquals(Data[1, 1], other.Data[1, 1])) return false;
            if (!Utils.IsEquals(Data[2, 0], other.Data[2, 0])) return false;
            if (!Utils.IsEquals(Data[2, 1], other.Data[2, 1])) return false;

            return true;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType())
            {
                if (!obj.GetType().IsSubclassOf(typeof(TransformationMatrix)))
                    return false;                
            }
            return Equals((TransformationMatrix)obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return Data?.GetHashCode() ?? 0;
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
                $"R0: ({Utils.Format(Data[0, 0])},{Utils.Format(Data[0, 1])}), R1: ({Utils.Format(Data[1, 0])},{Utils.Format(Data[1, 1])}), TX: {Utils.Format(Data[2, 0])}, TY: {Utils.Format(Data[2, 1])}";
        }

        #endregion
    }

    /// <summary>
    /// Матрица переноса
    /// </summary>
    public class TranslationMatrix : TransformationMatrix
    {
        /// <summary>
        /// Горизонтальный перенос
        /// </summary>
        private double TX
        {
            get { return Data[2, 0]; }
            set { Data[2, 0] = value; }
        }

        /// <summary>
        /// Вертикальный перенос
        /// </summary>
        private double TY
        {
            get { return Data[2, 1]; }
            set { Data[2, 1] = value; }
        }

        /// <summary>
        /// Создает матрицу переноса
        /// </summary>
        /// <param name="tx">Горизонтальный перенос</param>
        /// <param name="ty">Вертикальный перенос</param>
        public TranslationMatrix(double tx, double ty) : base()
        {
                TX = tx;
                TY = ty;
        }
    }

    /// <summary>
    /// Матрица вращения
    /// </summary>
    public class RotationMatrix : TransformationMatrix
    {
        /// <summary>
        /// Создает матрицу вращения на указанный угол
        /// </summary>
        /// <param name="alpha">Угол вращения</param>
        public RotationMatrix(double alpha)
        {
            RotationMatrixBySinCos(Math.Sin(alpha), Math.Cos(alpha));
        }

        private void RotationMatrixBySinCos(double sinalpha, double cosalpha)
        {
            Data[0, 0] = cosalpha;
            Data[0, 1] = sinalpha;
            Data[1, 0] = -sinalpha;
            Data[1, 1] = cosalpha;
        }
    }

    /// <summary>
    /// Матрица масштабирования
    /// </summary>
    public class ScaleMatrix : TransformationMatrix
    {
        /// <summary>
        /// Создает матрицу масштабирования с неравными пропорциями
        /// </summary>
        /// <param name="kx">Горизонтальный масштаб</param>
        /// <param name="ky">Вертикальный масштаб</param>
        public ScaleMatrix(double kx, double ky)
        {
                Data[0,0] = kx;
                Data[1,1] = ky;
        }

        /// <summary>
        /// Создает матрицу масштабирования с одинаковыми пропорциями
        /// </summary>
        /// <param name="k">Коэффицент масштабирования</param>
        public ScaleMatrix(double k):this(k,k)
        {}
    }
}
