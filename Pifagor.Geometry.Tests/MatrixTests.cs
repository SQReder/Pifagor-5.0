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
            var tm = TransformationMatrix.Rotate(Math.PI/2);
            var actual = v*tm;
            Assert.AreEqual(0, actual.X, Compare.AbsTol);
            Assert.AreEqual(1, actual.Y, Compare.AbsTol);
        }

        [Test]
        public void TranslateVector()
        {
            var v = new Vector(1, 0);
            var tm = TransformationMatrix.Translate(1, 2);
            var actual = v*tm;
            Assert.AreEqual(2,actual.X,Compare.AbsTol);
            Assert.AreEqual(2,actual.Y,Compare.AbsTol);
        }

        [Test]
        public void ScaleVector()
        {
            var v = new Vector(1, 1);
            var tm = TransformationMatrix.Scale(2, 3);
            var actual = v*tm;
            Assert.AreEqual(2, actual.X, Compare.AbsTol);
            Assert.AreEqual(3, actual.Y, Compare.AbsTol);
        }

        [Test]
        public void RotateComposition_EqualToAngleSummRotate()
        {
            var tm = TransformationMatrix.Rotate(Math.PI);
            var tmComposition = TransformationMatrix.Rotate(Math.PI/2)*TransformationMatrix.Rotate(Math.PI/2);
            Assert.That(tm, Is.EqualTo(tmComposition));
        }

        [Test]
        public void TransformComposition()
        {
            var expected = new Vector(-2, 0);

            var vector = new Vector(1,0);
            var transform = TransformationMatrix.Rotate(Math.PI);
            transform *= TransformationMatrix.Scale(2, 2);
            var actual = vector* transform;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void RotationByZero_EqualTo_NoopMatrix()
        {
            var rotation = TransformationMatrix.Rotate(0);
            var noop = TransformationMatrix.Noop();
            Assert.That(rotation, Is.EqualTo(noop));
        }

        [Test]
        public void TranslationByZero_EqualTo_NoopMatrix()
        {
            var translate = TransformationMatrix.Translate(0,0);
            var noop = TransformationMatrix.Noop();
            Assert.That(translate, Is.EqualTo(noop));
        }
    }
}
