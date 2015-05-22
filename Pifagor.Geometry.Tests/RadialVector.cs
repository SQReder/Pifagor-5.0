using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    public class RadialVectorTests
    {
        [Test]
        public void ZeroCoordinates()
        {
            var v = RadialVector.Zero;
            Assert.That(v.R, Is.EqualTo(0));
            Assert.That(v.A, Is.EqualTo(0));
        }
    }
}