using System;
using NUnit.Framework;

namespace Pifagor.Geometry.Tests
{
    [TestFixture]
    class TransformationChainTests
    {
        [Test]
        public void InitialState_EqualsTo_NoopMatrix()
        {
            var expected = TransformationMatrix.NoTransformation();

            var builder = new TransformationChainBuilder();
            var result = builder.Result;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void BuildOneTransform_EqualsTo_SameTransform()
        {
            var matrix = TransformationMatrix.ScaleMatrix(2, 2);

            var builder = new TransformationChainBuilder();
            var result = builder.Append(matrix).Result;
            
            Assert.That(result, Is.EqualTo(matrix));
        }

        [Test]
        public void BuildManyTransform_EqualsTo_CompositionOfTransformations()
        {
            var translate = TransformationMatrix.TranslationMatrix(1, 0);
            var rotate = TransformationMatrix.RotationMatrix(Math.PI);
            var scale = TransformationMatrix.ScaleMatrix(2, 2);
            var expected = translate * rotate * scale;

            var builder = new TransformationChainBuilder();
            builder.Append(translate).Append(rotate).Append(scale);
            var actual = builder.Result;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
