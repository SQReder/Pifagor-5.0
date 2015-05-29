using System;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using NUnit.Framework;

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
            var tm = TransformationMatrix.RotationMatrix(Math.PI);

            var actual = s*tm;
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
