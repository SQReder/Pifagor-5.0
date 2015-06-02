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
            var b = a + Utils.AbsTol / 2;

            Assert.That(Utils.IsEquals(a,b), Is.True);
        }

        [Test]
        public void ShouldNotBeEqual()
        {
            var a = 0.0;
            var b = a + Utils.AbsTol;

            Assert.That(Utils.IsEquals(a,b), Is.True);
        }
    }
}