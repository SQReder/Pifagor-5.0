using FakeItEasy;
using NUnit.Framework;
using Pifagor.Geometry;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public class TreeTests
    {
        [Test]
        public void AppendFind()
        {
            var tree = new Tree();
            var fake = A.Fake<FractalCluster>();
            tree.Insert(new[] {0,1,0,1}, fake);

            Assert.That(tree.Find(new[] {0}), Is.Null);
            Assert.That(tree.Find(new[] {0,1}), Is.Null);
            Assert.That(tree.Find(new[] {0,0,0}), Is.Null);
            Assert.That(tree.Find(new[] {0,1,0,1}), Is.EqualTo(fake));
        }
    }
}