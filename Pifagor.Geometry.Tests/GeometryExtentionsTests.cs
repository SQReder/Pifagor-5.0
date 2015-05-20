
using System;
using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class GeometryExtentionsTests
    {
        [Test]
        public void CreateVectorFromRadiusVector()
        {
            var v = new RadialVector(0, 0).ToVector();
            Assert.AreEqual(0, v.x, Compare.AbsTol);
            Assert.AreEqual(0, v.y, Compare.AbsTol);
        }

        [TestCase(0,0,0,0)]
        [TestCase(1,0,0,1)]
        [TestCase(0,1,Math.PI/2.0,1)]
        public void CreateRadialVectorFromVector(double x, double y, double expectedAngle, double expectedRadius)
        {
            var vector = new Vector(x, y).ToRadialVector();

            Assert.AreEqual(expectedAngle, vector.a, Compare.AbsTol);
            Assert.AreEqual(expectedRadius, vector.r, Compare.AbsTol);
        }

        [Test]
        public void TwoWayConversion_MakeSameResultAsSource(
            [Random(1)] double x,
            [Random(1)] double y
            )
        {
            var vector = new Vector(x, y).ToRadialVector().ToVector();

            Assert.AreEqual(x, vector.x, Compare.AbsTol);
            Assert.AreEqual(y, vector.y, Compare.AbsTol);
        }
    }
}