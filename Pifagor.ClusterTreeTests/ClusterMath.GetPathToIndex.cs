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
		[TestCase(2, 5, new[] {1,0})]
		[TestCase(2, 6, new[] {1,1})]
		[TestCase(2, 7, new[] {0,0,0})]
		[TestCase(2, 8, new[] {0,0,1})]
		[TestCase(2, 9, new[] {0,1,0})]
		[TestCase(2, 10, new[] {0,1,1})]
		[TestCase(2, 11, new[] {1,0,0})]
		[TestCase(2, 12, new[] {1,0,1})]
		[TestCase(2, 13, new[] {1,1,0})]
		[TestCase(2, 14, new[] {1,1,1})]
		[TestCase(3, 28, new[] {1,2,0})]
	    public void GetPathToIndex(int treeBase, int index, int[] expected)
	    {
	        Assert.That(ClusterMath.GetPathToIndex(treeBase, index), Is.EqualTo(expected));
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

	    [Test]
	    [TestCase(2, 0, new[] {0})]
	    [TestCase(2, 1, new[] {1})]
	    [TestCase(2, 2, new[] {1, 0})]
	    [TestCase(2, 3, new[] {1, 1})]
	    [TestCase(2, 4, new[] {1, 0, 0})]
	    [TestCase(2, 5, new[] {1, 0, 1})]
	    [TestCase(2, 6, new[] {1, 1, 0})]
	    [TestCase(2, 7, new[] {1, 1, 1})]
	    [TestCase(2, 8, new[] {1, 0, 0, 0})]
	    [TestCase(2, 9, new[] {1, 0, 0, 1})]
	    [TestCase(2, 10, new[] {1, 0, 1, 0})]
	    [TestCase(2, 11, new[] {1, 0, 1, 1})]
	    [TestCase(2, 12, new[] {1, 1, 0, 0})]
	    [TestCase(2, 13, new[] {1, 1, 0, 1})]
	    [TestCase(2, 14, new[] {1, 1, 1, 0})]
	    [TestCase(2, 15, new[] {1, 1, 1, 1})]
	    public void ConversionTest(int treeBase, int x, int[] expected)
	    {
            Assert.That(ClusterMath.ConvertNumberToBase(treeBase, x).ToArray(), Is.EqualTo(expected));
	    }
    }
}