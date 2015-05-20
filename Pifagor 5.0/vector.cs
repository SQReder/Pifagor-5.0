using System;
using System.Drawing;
using Newtonsoft.Json;

namespace SQReder.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Vector
    {
        [JsonProperty]
        public float a;

        [JsonProperty]
        public float r;

        public float GetAngle { get { return a; } }
        public float GetRadius { get { return r; } }
        public Vector GetNormalised { get { return new Vector(1, a); } }

        public PointF Point
        {
            get { return new PointF { X = (float)(r * Math.Cos(a)), Y = (float)(r * Math.Sin(a)) }; }
        }

        public SizeF ToSizeF()
        {
            return new SizeF(Point); 
        }

        public Vector()
            : this(new PointF())
        { }

        public Vector(PointF p)
        {
            r = (float)Math.Sqrt(p.X * p.X + p.Y * p.Y);
            if (Math.Abs(p.X - 0) < 0.001)
                a = (float)(Math.PI / 2d * Math.Sign(p.Y));
            else
                a = (float)(Math.Atan(p.Y / p.X) + ((p.X < 0) ? Math.PI : 0));
        }

        public Vector(float r, float a)
        {
            this.r = r;
            this.a = a % (float)(2 * Math.PI);
        }

        public Vector(PointF beginPointF, PointF endPointF)
            : this(new PointF(endPointF.X - beginPointF.X, endPointF.Y - beginPointF.Y))
        {
            //var relativePos = new PointF(endPointF.X - beginPointF.X, endPointF.Y - beginPointF.Y);
            //var v = new Vector(relativePos);
            //r = v.GetRadius;
            //a = v.GetAngle;
        }

        #region Перегрузка операторов
        public static bool operator ==(Vector a, Vector b)
        {
            //if (a == null && b == null)
            //    return true;
            //if (a == null || b == null)
            //    return false;
            return (Math.Abs(a.r) < 0.00001 && Math.Abs(b.r) < 0.00001) ||
                   (Math.Abs(a.a - b.a) < 0.00001 && Math.Abs(a.r - b.r) < 0.00001);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            //if (a == null && b == null)
            //    return false;
            //if (a == null || b == null)
            //    return true;
            return (Math.Abs(a.r) >= 0.00001 || Math.Abs(b.r) >= 0.00001) &&
                   (Math.Abs(a.a - b.a) >= 0.00001 || Math.Abs(a.r - b.r) >= 0.00001);
        }

        public static Vector operator *(Vector v, float k)
        {
            return new Vector(v.r * k, v.a);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            var A = a.Point;
            var B = new SizeF(b.Point);
            return new Vector(A + B);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var v = (Vector)obj;
            return v == this;
        }
        #endregion Перегрузка операторов

        public float AngleDifference(Vector v)
        {
            return (float)(((((a - v.a) % 2 * Math.PI) + 3 * Math.PI) % 2 * Math.PI) - Math.PI);
        }

        public override string ToString()
        {
            return "R=" + r.ToString("G") + ",A=" + a.ToString("G");
        }
        public string ToString(string formatString)
        {
            return "R=" + r.ToString(formatString) + ",A=" + a.ToString(formatString);
        }

        public override int GetHashCode()
        {
            return r.GetHashCode() ^ a.GetHashCode();
        }

    }
}
