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
            Assert.AreEqual(1, matrixVector.Y, Compare.AbsTol);
        }
    }
}
