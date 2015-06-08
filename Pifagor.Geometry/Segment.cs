using System;
using System.Diagnostics.CodeAnalysis;

namespace Pifagor.Geometry
{
    class Segment
    {

        public Segment(Vector begin, Vector end)
        {
            Begin = begin;
            End = end;
        }

        public Vector Begin { get; }
        public Vector End { get; }
        public Vector RelativeVector => End - Begin;

        [Obsolete]
        public static Segment operator *(Segment seg, TransformationMatrix tm)
        {
            return new Segment(seg.Begin * tm, seg.End * tm);
        }

        public TransformationMatrix ToTransformationMatrix()
        {
            var angle = RelativeVector.Angle();
            var scale = RelativeVector.Length;

            return new TranslationMatrix(Begin.X, Begin.Y)
                   *new RotationMatrix(angle)
                   *new ScaleMatrix(scale);
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

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                return (Begin.GetHashCode() *397) ^ End.GetHashCode();
            }
        }

        #endregion

        #region Formatting members

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Begin: {Begin}, End: {End}";
        }

        #endregion
    }
}
