using System;
using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void RotateVector()
        {
            var v = new Vector(1,0);
            var tm = TransformationMatrix.RotationMatrix(Math.PI/2);
            var actual = v*tm;
            Assert.AreEqual(0, actual.X, Compare.AbsTol);
            Assert.AreEqual(1, actual.Y, Compare.AbsTol);
        }

        [Test]
        public void TranslateVector()
        {
            var v = new Vector(1, 0);
            var tm = TransformationMatrix.TranslationMatrix(1, 2);
            var actual = v*tm;
            Assert.AreEqual(2,actual.X,Compare.AbsTol);
            Assert.AreEqual(2,actual.Y,Compare.AbsTol);
        }
        }

        [Test]
        public void RotateComposition_EqualToAngleSummRotate()
        {
            var tm = TransformationMatrix.RotationMatrix(Math.PI);
            var tmComposition = TransformationMatrix.RotationMatrix(Math.PI/2)*TransformationMatrix.RotationMatrix(Math.PI/2);
            Assert.That(tm, Is.EqualTo(tmComposition));
        }

        [Test, Ignore]
        public void TranslateAndRotateComposition()
        {
            Assert.Fail();   
        }
    }
}
