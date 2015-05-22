using System;
using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public partial class ClusterMathTests
    {
        [Test]
        [TestCase(1,0,0)]
        [TestCase(1,1,1)]
        [TestCase(1,10,10)]
        [TestCase(2,0,0)]
        [TestCase(2,1,1)]
        [TestCase(2,2,3)]
        [TestCase(2,3,7)]
        [TestCase(2,10,1023)]
        public void GetFirstIndexOfLayer(int treeBase, int layer, int expected)
        {
            var index = ClusterMath.GetFirstIndexOfLayer(treeBase, layer);
            Assert.That(index, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(0, 1, TestName = "Tree base equal to zero")]
        [TestCase(-1, 1, TestName = "Tree base less than zero")]
        [TestCase(1, -1, TestName = "Index less than zero")]
        [TestCase(1, -1, TestName = "Index less than zero")]
        public void GetFirstIndexOfLayer_Trow_OutOfRangeException(int treeBase, int layer)
        {
            try
            {
                ClusterMath.GetFirstIndexOfLayer(treeBase, layer);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.That(e, Is.TypeOf<ArgumentOutOfRangeException>());
            }
        }    
    }
}