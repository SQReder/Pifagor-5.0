namespace Pifagor.Geometry
{
    public class Segment
    {
        public Vector Begin { get; }
        public Vector End { get; }

        public Segment(Vector begin, Vector end)
        {
            Begin = begin;
            End = end;
        }

        public Segment TransformWith(Segment segment)
        {
            var translate = new TranslationMatrix(segment.Begin.X, segment.Begin.Y);
            var tmBegin = segment.Begin.GetBaseVectorTransformation();
            var tmEnd= segment.End.GetBaseVectorTransformation();
            var tm = (segment.End - segment.Begin).GetBaseVectorTransformation();
            var begin = Begin*tm;
            var end = End*tm;
            begin *= translate;
            end *= translate;
            return new Segment(begin, end);
        }

        private TransformationMatrix GetTranslationMatrix()
        {
            return new TranslationMatrix(Begin.X, Begin.Y);
        }

        private TransformationMatrix GetRotationMatrix()
        {
            return new RotationMatrix((End - Begin).Angle());
        }

        private TransformationMatrix GetScaleMatrix()
        {
            return new ScaleMatrix((End-Begin).Length);
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

        #region Formattin members 

        public override string ToString()
        {
            return $"{Begin} - {End}";
        }

        #endregion
    }
}
