﻿using System.Drawing;

namespace Pifagor.Geometry
{
    public class Segment: IDrawable
    {
        public Vector Begin { get; }
        public Vector End { get; }
        public double Length { get; }

        public Segment(double x1, double y1, double x2, double y2)
            : this(new Vector(x1, y1), new Vector(x2, y2))
        { }

        public Segment(Vector begin, Vector end)
        {
            Begin = begin;
            End = end;
            Length = Begin.Distance(End);
        }

        public Segment TransformWith(Segment segment)
        {
            var translate = new TranslationMatrix(segment.Begin.X, segment.Begin.Y);
            var relativeVector = segment.End - segment.Begin;
            var tm = relativeVector.GetBaseVectorTransformation()*translate;
            var begin = Begin*tm;
            var end = End*tm;
            return new Segment(begin, end);
        }
        
        #region Equality members

        protected bool Equals(Segment other)
        {
            return Equals(Begin, other.Begin) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Segment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Begin.GetHashCode() *397) ^ End.GetHashCode());
            }
        }

        public static bool operator ==(Segment left, Segment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Segment left, Segment right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region IDrawable members

        public void Draw(Graphics g, Pen pen)
        {
            g.DrawLine(pen, Begin, End);
        }

        #endregion

        #region Formattin members 

        public override string ToString()
        {
            return $"{Begin} - {End}";
        }

        #endregion
    }
}
