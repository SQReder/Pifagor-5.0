using System;
using NUnit.Framework;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public partial class ClusterMathTests
    {
        [Test]
        public void TestMaxLinearSize()
        {
            var fc = new FractalCluster();
            var segment = new Segment(0, 0, 1, 1);
            fc.Segments.Add(segment);

            var actual = ClusterMath.GetMaxLinearSize(fc);
            var expected = Math.Sqrt(2);
            Assert.That(actual, Is.EqualTo(expected));
        }


    }
}
