using System;
using System.Collections;
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
            Assert.AreEqual(0, v.X, Compare.AbsTol);
            Assert.AreEqual(0, v.Y, Compare.AbsTol);
        }

        [TestCase(0,0,0,0)]
        [TestCase(1,0,0,1)]
        [TestCase(0,1,Math.PI/2.0,1)]
        public void CreateRadialVectorFromVector(double x, double y, double expectedAngle, double expectedRadius)
        {
            var vector = new Vector(x, y).ToRadialVector();

            Assert.AreEqual(expectedAngle, vector.A, Compare.AbsTol);
            Assert.AreEqual(expectedRadius, vector.R, Compare.AbsTol);
        }

        [Test, Combinatorial]
        public void TwoWayConversion_MakeSameResultAsSource(
            [Values(0,1,-1,Math.PI, -Math.PI)]    double x,
            [Values(0, 1, -1, Math.PI, -Math.PI)] double y)
        {
            var vector = new Vector(x, y).ToRadialVector().ToVector();

            Assert.AreEqual(x, vector.X, Compare.AbsTol);
            Assert.AreEqual(y, vector.Y, Compare.AbsTol);
        }

        [Test]
        public void UnitReturnZeroVector_ForZeroVector()
        {
            var unit = new Vector(0, 0).Unit();
            Assert.AreEqual(0, unit.Length, Compare.AbsTol);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(-1,0)]
        public void GetUnit_FromVector(double x, double y)
        {
            var unit = new Vector(x, y).Unit();
            Assert.AreEqual(1, unit.Length, Compare.AbsTol);
        }

        [Test]
        public void GetUnit_FromRadialVector()
        {
            var unit = new RadialVector(10, 0).Unit();
            Assert.That(unit.R, Is.EqualTo(1));
        }
    }
}