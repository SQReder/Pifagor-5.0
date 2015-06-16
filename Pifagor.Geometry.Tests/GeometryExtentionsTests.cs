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

        //[Test]
        //[TestCase(0,0,0,0, TestName = "Zero")]
        //[TestCase(0,0,1,1, TestName = "45")]
        //[TestCase(0,0,-1,1, TestName = "135")]
        //[TestCase(0,0,-1,-1, TestName = "215")]
        //[TestCase(0,0,1,-1, TestName = "305")]
        //[TestCase(0,0,2,2, TestName = "45, l2")]
        //[TestCase(1,1,2,1, TestName = "0, l1")]
        //public void GetTransfromationMatrixForTwoPoints(double x1, double y1, double x2, double y2)
        //{
        //    var x = x2 - x1;
        //    var y = y2 - y1;
        //    var expected = new Vector(x, y);

        //    var transfrormation = GeometryExtensions.GetBaseVectorTransformation(x1, y1, x2, y2);
        //    var actual = transfrormation.Apply(Vector.Base);
        //    Assert.That(actual, Is.EqualTo(expected));
        //}
    }
}