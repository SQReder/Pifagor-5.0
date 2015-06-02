using System;
using System.Collections;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    class SegmentTests
    {
        [Test]
        public void InitialState()
        {
            var begin = A.Fake<Vector>();
            var end = A.Fake<Vector>();
            var s = new Segment(begin, end);
            Assert.That(s.Begin, Is.EqualTo(begin));
            Assert.That(s.End, Is.EqualTo(end));
        }

        [Test]
        public void ApplyTransformationMatrix()
        {
            var expected = new Segment(new Vector(-1,0), new Vector(0,-1));

            var s = new Segment(new Vector(1,0), new Vector(0,1));
            var tm = TransformationMatrix.Rotation(Math.PI);

            var actual = s*tm;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetTransformationMatix_BasedOnZeroCoordinates()
        {
            var tm = TransformationMatrix.Translation(1, 1)
                           *TransformationMatrix.Rotation(Math.PI/2.0)
                           *TransformationMatrix.Scaling(1/2.0)
                           *TransformationMatrix.Translation(-1/2.0,0);
            var expected = Vector.Base*tm;

            var segment = new Segment(Vector.Zero, new Vector(-1, 1));
            var matrix = segment.ToTransformationMatrix();
            var actual = Vector.Base*matrix;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetTransformationMatix()
        {
            var begin = new Vector(1,1);
            var end = new Vector(1, 0.5);

            var segment = new Segment(begin, end);
            var matrix = segment.ToTransformationMatrix();
            var actual = Vector.Base*matrix;

            Assert.That(actual, Is.EqualTo(segment.RelativeVector));
            Assert.That(begin + actual, Is.EqualTo(end));
        }

        [Test, TestCaseSource(typeof (TransformationTestCaseDataSource), "TestCases")]
        public void TransformationMatrixTest(Vector begin, Vector end)
        {
            var segment = new Segment(begin, end);
            var matrix = segment.ToTransformationMatrix();
            var actual = Vector.Base * matrix;

            Assert.That(actual, Is.EqualTo(segment.RelativeVector), matrix.ToString());
        }

        [Test, TestCaseSource(typeof(TransformationTestCaseDataSource), "TestCases")]
        public void TestRelativeVector(Vector begin, Vector end)
        {
            var segment = new Segment(begin, end);
            Assert.That(segment.Begin + segment.RelativeVector, Is.EqualTo(segment.End));
        }
    }

    public class TransformationTestCaseDataSource
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(Vector.Zero, Vector.Zero);
                yield return new TestCaseData(Vector.Zero, Vector.Base);
                yield return new TestCaseData(Vector.Base, Vector.Base);
                yield return new TestCaseData(new Vector(1, 1), new Vector(2,2));
            }
        }
    }
}
