using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pifagor.Geometry
{
    class MatrixVector
    {
        private readonly double[,] _data = new double[3,3];

        public MatrixVector()
        {
            this[3, 3] = 1;
        }

        public MatrixVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double this[int row, int col]
        {
            get { return _data[row, col]; }
            set { _data[row, col] = value; }
        }

        public double Y
        {
            get { return _data[1, 1]; }
            set { _data[1, 1] = value; }
        }


        public double X
        {
            get { return _data[0, 0]; }
            set { _data[0, 0] = value; }
        }
    }
}
