using System;
using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public partial class ClusterMathTests
    {
        [Test]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 10, 10)]
        [TestCase(1, 100, 100)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 10, 3)]
        [TestCase(2, 100, 6)]
        [TestCase(3, 4, 2)]
        [TestCase(3, 12, 2)]
        [TestCase(3, 13, 3)]
        public void GetLayerNumber(int treeBase, int index, int expected)
        {
            var layerNumber = ClusterMath.GetLayerNumber(treeBase, index);
            Assert.That(layerNumber, Is.EqualTo(expected),
                $"Cluster {index} expected at level {expected}, but {layerNumber} received");
        }

        [Test]
        [TestCase(0,1, TestName = "Tree base equal to zero")]
        [TestCase(-1,1, TestName = "Tree base less than zero")]
        [TestCase(1,-1, TestName = "Index less than zero")]
        public void GetLayerNumber_Trow_OutOfRangeException(int treeBase, int index)
        {
            try
            {
                ClusterMath.GetLayerNumber(treeBase, index);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.That(e, Is.TypeOf<ArgumentOutOfRangeException>());
            }
        }
    }
}