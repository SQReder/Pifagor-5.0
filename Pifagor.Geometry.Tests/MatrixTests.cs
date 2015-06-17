using System;
using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        public object[] TranslateTestCases = new object[]
        {
            new object[] { 0,0,1,1,1,1},
            new object[] { 0,0,-1,-2,-1,-2},
            new object[] { 1,2,3,4,4,6 },
        };

        [Test]
        [TestCaseSource(nameof(TranslateTestCases))]
        public void Translation(double x, double y, double tx, double ty, double exx, double exy)
        {
            var expected = new Vector(exx, exy);

            var v = new Vector(x, y);
            var actual = v * new TranslationMatrix(tx, ty);

            Assert.That(actual, Is.EqualTo(expected));
        }


        private const double Deg0 = 0;
        private const double Deg90 = Math.PI / 2;
        private const double Deg180 = Math.PI;
        private const double Deg270 = Math.PI * 3 / 2;
        private const double Deg360 = Math.PI * 2;

        [Test]
        [TestCase(Deg90, 0, 0, 0, 0, TestName = "Rotate zero vector")]
        [TestCase(Deg90, 1, 0, 0, 1, TestName = "Rotate (1,0) -> 90")]
        [TestCase(Deg90, 1, 1, -1, 1, TestName = "Rotate (1,1) -> 90")]
        [TestCase(Deg270, 1, 1, 1, -1, TestName = "Rotate (1,1) -> 270")]
        [TestCase(Deg180, 2, 1, -2, -1, TestName = "Rotate (2,1) -> 180")]
        [TestCase(Deg360, 1, 1, 1, 1, TestName = "Rotate (1,1) -> 360")]
        [TestCase(Deg360, 1, 1, 1, 1, TestName = "Rotate (1,1) -> 0")]
        public void RotateVector(double alpha, double x, double y, double expectedX, double expectedY)
        {
            var expected = new Vector(expectedX, expectedY);

            var v = new Vector(x, y);
            var tm = new RotationMatrix(alpha);
            var actual = v * tm;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TranslateVector()
        {
            var expected = new Vector(2, 2);

            var v = new Vector(1, 0);
            var tm = new TranslationMatrix(1, 2);
            var actual = v * tm;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void NonproportionalScaleVector()
        {
            var expected = new Vector(2, 3);

            var v = new Vector(1, 1);
            var tm = new ScaleMatrix(2, 3);
            var actual = v * tm;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ProportionalScaleVector()
        {
            var expected = new Vector(3.5, 3.5);

            var v = new Vector(1, 1);
            var tm = new ScaleMatrix(3.5);
            var actual = v * tm;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RotateComposition_EqualToAngleSummRotate()
        {
            var tm = new RotationMatrix(Deg180);
            var tmComposition = new RotationMatrix(Deg90) * new RotationMatrix(Deg90);
            Assert.That(tm, Is.EqualTo(tmComposition));
        }

        [Test]
        public void TransformComposition()
        {
            var expected = new Vector(-2, 0);

            var vector = new Vector(1, 0);
            var transform = new RotationMatrix(Deg180)*new ScaleMatrix(2, 2);
            var actual = vector * transform;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
