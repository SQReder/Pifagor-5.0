using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void CreateMatrixWithPreserveCoordinates()
        {
            var matrixVector = new MatrixVector(1,2);
            Assert.AreEqual(1, matrixVector.X, Compare.AbsTol);
            Assert.AreEqual(2, matrixVector.Y, Compare.AbsTol);
        }

        [Test]
        public void Addition()
        {
            var m1 = new MatrixVector(1,2,3,4);
            var m2 = new MatrixVector(5,6,7,8);
            var expected = new MatrixVector(6,8,10,12);

            var actual = m1 + m2;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
