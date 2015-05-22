namespace Pifagor.Geometry
{
    public struct RadialVector
    {
        public readonly double R;
        public readonly double A;

        public RadialVector(double r, double a)
        {
            R = r;
            A = a;
        }

        public static RadialVector Zero => new RadialVector(0,0);
    }
}
