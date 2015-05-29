using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void VectorAddition()
        {
            var a = new Vector(1,1);
            var b = new Vector(1,2);
            var c = a + b;

            Assert.AreEqual(2,c.X, Compare.AbsTol);
            Assert.AreEqual(3,c.Y, Compare.AbsTol);
        }

        [Test]
        public void VectorSubstraction()
        {
            var a = new Vector(1,1);
            var b = new Vector(1,2);
            var c = a - b;

            Assert.AreEqual(0,c.X, Compare.AbsTol);
            Assert.AreEqual(-1,c.Y, Compare.AbsTol);
        }

        [Test]
        public void MultiplyByVector()
        {
            var a = new Vector(2, 1);
            var b = new Vector(1, 2);
            var c = a*b;

            Assert.AreEqual(4, c, Compare.AbsTol);
        }

        [Test]
        public void MultiplyByScalar()
        {
            var v = new Vector(1,1) * 10;
            Assert.That(v.X, Is.EqualTo(10));
            Assert.That(v.Y, Is.EqualTo(10));
        }

        [Test]
        public void ZeroCoordinates()
        {
            var v = Vector.Zero;
            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
        }
    }
}