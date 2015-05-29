using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
	[TestFixture]
    public partial class ClusterMathTests
    {
		[Test]
		[TestCase(2, 0, new int[0])]
		[TestCase(2, 1, new[] {0})]
		[TestCase(2, 2, new[] {1})]
		[TestCase(2, 3, new[] {0,0})]
		[TestCase(2, 4, new[] {0,1})]
		[TestCase(3, 28, new[] {1,2,0})]
	    public void GetPathToIndex(int treeBase, int index, int[] expected)
	    {
	        Assert.That(ClusterMath.GetPathToIndex(treeBase, index), Is.EquivalentTo(expected));
	    }

	    [Test]
        [TestCase(2, 6, 3)]
        [TestCase(2, 7, 0)]
        [TestCase(2, 8, 1)]
        [TestCase(2, 9, 2)]
        [TestCase(2, 10, 3)]
        [TestCase(2, 11, 4)]
        [TestCase(2, 12, 5)]
        public void GetInternalNumber(int treeBase, int index, int expected)
	    {
	        var layerNumber = ClusterMath.GetLayerNumber(treeBase, index);
	        var firstIndexOfLayer = ClusterMath.GetFirstIndexOfLayer(treeBase, layerNumber);
	        Assert.That(index - firstIndexOfLayer, Is.EqualTo(expected));
	    }
    }
}