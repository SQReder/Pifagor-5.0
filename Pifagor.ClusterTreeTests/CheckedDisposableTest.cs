using System;
using FakeItEasy;
using NUnit.Framework;

namespace Pifagor.ClusterTree.Tests
{
    [TestFixture]
    public class CheckedDisposableTest
    {
        private static CheckedDisposable MakeFakeCheckedDisposable()
        {
            return A.Fake<CheckedDisposable>(options => options.CallsBaseMethods());
        }

        [Test]
        public void MustNot_ThrowException_FirstDisposeCall()
        {
            var fake = MakeFakeCheckedDisposable();
            fake.Dispose();
        }

        [Test]
        public void MustThrowObjectDisposedException_OnSecondDisposeCall()
        {
            var fake = MakeFakeCheckedDisposable();
            fake.Dispose();

            var exceptionThrowed = false;
            try
            {
                fake.Dispose();
            }
            catch (ObjectDisposedException)
            {
                exceptionThrowed = true;
            }
            Assert.That(exceptionThrowed, Is.True);
        }
    }
}