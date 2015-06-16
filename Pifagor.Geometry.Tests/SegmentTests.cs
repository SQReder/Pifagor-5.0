using System;
using System.Collections;
using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    class SegmentTests
    {
        [Test, TestCaseSource(typeof (TransfromTestDataSource), nameof(TransfromTestDataSource.TransformTestCaseData))]
        public void TransformTest(Segment a, Segment b, Segment expected)
        {
            var actual = a.TransformWith(b);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AccumulateTransformations()
        {
            var a = new Segment(new Vector(1, 2), new Vector(3, 4));
            var b = new Segment(new Vector(5, 6), new Vector(9, 10));
            var accumulate = a.TransformWith(b);

            var seg = new Segment(new Vector(0.5, 0.5), new Vector(1, 0));

            var actual = seg.TransformWith(accumulate);
            var expected = seg.TransformWith(a).TransformWith(b);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    public class TransfromTestDataSource
    {
        private static TestCaseData MakeTestCaseData(
            double x1, double y1, double x2, double y2, double x3, double y3,
            double x4, double y4, double x5, double y5, double x6, double y6,
            string testName = null)
        {
            var testCaseData = new TestCaseData(
                new Segment(new Vector(x1, y1), new Vector(x2, y2)),
                new Segment(new Vector(x3, y3), new Vector(x4, y4)),
                new Segment(new Vector(x5, y5), new Vector(x6, y6))
                );
            testCaseData.SetName(testName);
            return testCaseData;
        }

        public static IEnumerable TransformTestCaseData
        {
            get
            {
                yield return MakeTestCaseData(0, 0, 1, 0,   0, 0, 1, 0,     0, 0, 1, 0, "Invariant");

                yield return MakeTestCaseData(0, 0, 1, 0,   1, 2, 3, 4,     1, 2, 3, 4);
                yield return MakeTestCaseData(0, 0, 1, 0,   0, 0, 0, 0,     0, 0, 0, 0);
                yield return MakeTestCaseData(0, 0, 1, 0,   0, 0, 0, 1,    0, 0, 0, 1, "90 deg");
                yield return MakeTestCaseData(2, 3, 4, 5,   0, 0, -1, 0,    -2, -3, -4, -5, "180 deg");

                yield return MakeTestCaseData(0, 0, 2, 0,   0, 0, 0.5, 0,   0, 0, 1, 0);
                yield return MakeTestCaseData(0, 0, 2, 0,   0, 0, 1, 0,   0, 0, 2, 0);

                yield return MakeTestCaseData(1, 0, 1, 1,   1, 0, 1, 1,     1, 1, 0, 1);
                yield return MakeTestCaseData(1, 1, 2, 2,   0, 0, -1, 0,    -1, -1, -2, -2);
            }
        }
    }
}
