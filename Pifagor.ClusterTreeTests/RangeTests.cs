using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    class RangeTests
    {
        [Test]
        public void Enumerable()
        {
            var range = new Range {Begin = 10, Count = 5};
            var actual = range.ToList();
            var expected = new List<int> {10, 11, 12, 13, 14};
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}