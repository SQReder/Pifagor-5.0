using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    class ClusterTests
    {
        [Test]
        public void CanBeConstructed()
        {
            var cluster = new FractalCluster();
            Assert.That(cluster.Segments.Count, Is.EqualTo(0));
        }
    }
}
