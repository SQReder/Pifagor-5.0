using System;
using System.Collections;
using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class GeometryExtentionsTests
    {
        [Test]
        public void UnitReturnZeroVector_ForZeroVector()
        {
            var unit = new Vector(0, 0).Unit();
            Assert.AreEqual(0, unit.Length, Utils.AbsTol);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(-1,0)]
        public void GetUnit_FromVector(double x, double y)
        {
            var unit = new Vector(x, y).Unit();
            Assert.AreEqual(1, unit.Length, Utils.AbsTol);
        }
    }
}