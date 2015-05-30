using System;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public class TreeTests
    {
        interface INode
        {
            void DoSmth();
        }

        [Test]
        public void CreateNodeWithNoData()
        {
            var tree = new Tree<object>(null);
            Assert.That(tree.Parent, Is.Null);
            Assert.That(tree.Children, Is.Empty);
        }

        [Test]
        public void AttachNode()
        {
            var root = new Tree<object>(null);
            var child = new Tree<object>(null);
            root.Attach(child);
            Assert.That(root.Children.Count, Is.EqualTo(1));
            Assert.That(root.Children[0], Is.EqualTo(child));
            Assert.That(child.Parent, Is.EqualTo(root));
        }

        [Test]
        public void DetachTree()
        {
            var root = new Tree<object>(null);
            var child = new Tree<object>(null);
            root.Attach(child);
            root.Detach(child);

            Assert.That(root.Children.Count, Is.EqualTo(0));
            Assert.That(child.Parent, Is.Null);
        }

        [Test]
        public void Find_ThisNode()
        {
            var root = new Tree<int>(1);
            var result = root.Find(p => p == 1);
            Assert.That(result, Is.EqualTo(root));
            Assert.That(result.Data, Is.EqualTo(1));
        }

        [Test]
        public void Find_ImmediateChild()
        {
            var root = new Tree<int>(0);
            var child1 = new Tree<int>(1);
            var child2 = new Tree<int>(2);
            root.Attach(child1);
            root.Attach(child2);

            var result = root.Find(p => p == 2);

            Assert.That(result, Is.EqualTo(child2));
            Assert.That(result.Data, Is.EqualTo(2));
        }

        [Test]
        public void Find_ReturnNull_IfNothingFound()
        {
            var root = new Tree<int>(0);
            var child = new Tree<int>(1);
            root.Attach(child);

            var result = root.Find(p => p == 2);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ChildDispose_MustBeCalled()
        {
            var root = new Tree<int>(0);
            var child = A.Fake<Tree<int>>(o => o.WithArgumentsForConstructor(() => new Tree<int>(1)));
            root.Attach(child);

            root.Dispose();

            child.CallsTo(a => a.Dispose()).MustHaveHappened();
        }
    }
}