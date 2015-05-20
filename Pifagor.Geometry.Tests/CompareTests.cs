using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class CompareTests
    {
        [Test]
        public void ShouldBeEqual()
        {
            var a = 0.0;
            var b = a + Compare.AbsTol / 2;

            Assert.That(Compare.IsEquals(a,b), Is.True);
        }

        [Test]
        public void ShouldNotBeEqual()
        {
            var a = 0.0;
            var b = a + Compare.AbsTol;

            Assert.That(Compare.IsEquals(a,b), Is.True);
        }
    }
}