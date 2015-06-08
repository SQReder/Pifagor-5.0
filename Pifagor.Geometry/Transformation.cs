namespace Pifagor.Geometry
{
    class Transformation
    {
        public TranslationMatrix Translate { get; set; }
        public RotationMatrix Rotate { get; set; }
        public ScaleMatrix Scale { get; set;  }

        public Vector Apply(Vector v)
        {
            return v*Translate*Rotate*Scale;
        }
    }
}
