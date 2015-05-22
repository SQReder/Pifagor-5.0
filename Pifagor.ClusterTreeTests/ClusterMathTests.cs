using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public class ClusterMathTests
    {
        [Test]
        [TestCase(1,0,0)]
        [TestCase(1,1,1)]
        [TestCase(1,10,10)]
        [TestCase(1,100,100)]
        [TestCase(2,0,0)]
        [TestCase(2,1,1)]
        [TestCase(2,10,3)]
        [TestCase(2,100,6)]
        [TestCase(3,4,2)]
        [TestCase(3,12,2)]
        [TestCase(3,13,3)]
        public void GetLayerNumberTest(int treeBase, int index, int expected)
        {
            var layerNumber = ClusterMath.GetLayerNumber(index,treeBase);
            Assert.That(layerNumber, Is.EqualTo(expected),
                $"Cluster {index} expected at level {expected}, but {layerNumber} received");
        }
    }
}