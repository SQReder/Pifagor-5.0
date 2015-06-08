//using System;
//using NUnit.Framework;

//namespace Pifagor.Geometry.Tests
//{
//    [TestFixture]
//    class TransformationChainTests
//    {
//        [Test]
//        public void InitialState_EqualsTo_NoopMatrix()
//        {
//            var expected = NewTransformationMatrix.Noop;

//            var builder = new TransformationChainBuilder();
//            var result = builder.Result;

//            Assert.That(result, Is.EqualTo(expected));
//        }

//        [Test]
//        public void BuildOneTransform_EqualsTo_SameTransform()
//        {
//            var matrix = NewTransformationMatrix.Scaling(2, 2);

//            var builder = new TransformationChainBuilder();
//            var result = builder.Append(matrix).Result;
            
//            Assert.That(result, Is.EqualTo(matrix));
//        }

//        [Test]
//        public void BuildManyTransform_EqualsTo_CompositionOfTransformations()
//        {
//            var translate = NewTransformationMatrix.Translation(1, 0);
//            var rotate = NewTransformationMatrix.Rotation(Math.PI);
//            var scale = NewTransformationMatrix.Scaling(2, 2);
//            var expected = translate * rotate * scale;

//            var builder = new TransformationChainBuilder();
//            builder.Append(translate).Append(rotate).Append(scale);
//            var actual = builder.Result;

//            Assert.That(actual, Is.EqualTo(expected));
//        }
//    }
//}
