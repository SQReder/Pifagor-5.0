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
        public void AccumulateTransformation_MakeSameResult_AsSequentalTransformations()
        {
            var a = new Segment(new Vector(1, 2), new Vector(3, 4));
            var b = new Segment(new Vector(5, 6), new Vector(9, 10));
            var accumulate = a.TransformWith(b);

            var seg = new Segment(new Vector(0.5, 0.5), new Vector(1, 0));

            var actual = seg.TransformWith(accumulate);
            var expected = seg.TransformWith(a).TransformWith(b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test()
        {
            var a = new Vector(0,1);
            var b = new Vector(0.5,1.5);
            var c = new Vector(1,1);

            var ab = new Segment(a,b);
            var bc = new Segment(b,c);

            var abab = ab.TransformWith(ab);
            var ababab = ab.TransformWith(ab).TransformWith(ab);
            var bcbc = bc.TransformWith(bc);
            var bcbcbc = bc.TransformWith(bc).TransformWith(bc);

            Assert.That(abab, Is.EqualTo(new Segment(new Vector(-0.5,1.5), new Vector(-0.5,2))), "abab failed");
            Assert.That(ababab, Is.EqualTo(new Segment(new Vector(-1,1.5), new Vector(-1.25,1.75))), "ababab failed");
            Assert.That(bcbc, Is.EqualTo(new Segment(new Vector(1.5,2), new Vector(1.5,1.5))), "bcbc failed");
            Assert.That(bcbcbc, Is.EqualTo(new Segment(new Vector(2.25, 1.75), new Vector(2, 1.5))), "bcbcbc failed");

            //var ab_bc = ab.TransformWith(bc);
            var bc_ab = bc.TransformWith(ab);
            var ab_bc_ab = ab.TransformWith(bc).TransformWith(ab);
            //Assert.That(ab_bc, Is.EqualTo(new Segment(new Vector(-0.5, 2), new Vector(0,2))));
            Assert.That(bc_ab, Is.EqualTo(new Segment(new Vector(1, 2), new Vector(1.5,2))));
            Assert.That(ab_bc_ab, Is.EqualTo(new Segment(new Vector(-0.5,2.5), new Vector(-0.25, 2.75))));


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
