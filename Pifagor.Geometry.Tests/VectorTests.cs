using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class VectorTests
    {
        [Test]
        public void VectorAddition()
        {
            var expected = new Vector(11, 24);

            var a = new Vector(1,3);
            var b = new Vector(10,21);
            var actual = a + b;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void VectorSubstraction()
        {
            var expected = new Vector(-4,-4);

            var a = new Vector(1,3);
            var b = new Vector(5,7);
            var actual = a - b;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyByVector()
        {
            var a = new Vector(2, 1);
            var b = new Vector(1, 2);
            var c = a*b;

            Assert.AreEqual(4, c, Utils.AbsTol);
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