using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitCalculator.Tests
{
    [TestClass]
    public class QuaternionTensorTests
    {
        [TestMethod]
        public void TestConstructorWithDimensions()
        {
            var tensor = new QuaternionTensor(2, 3);
            Assert.AreEqual(2, tensor.Tensor.GetLength(0));
            Assert.AreEqual(3, tensor.Tensor.GetLength(1));
        }

        [TestMethod]
        public void TestConstructorWithArray()
        {
            double[,] array = { { 1, 2 }, { 3, 4 } };
            var tensor = new QuaternionTensor(array);
            Assert.AreEqual(array, tensor.Tensor);
        }

        [TestMethod]
        public void TestIndexerGetSet()
        {
            var tensor = new QuaternionTensor(2, 2);
            tensor[0, 0] = 1.0;
            tensor[1, 1] = 2.0;
            Assert.AreEqual(1.0, tensor[0, 0]);
            Assert.AreEqual(2.0, tensor[1, 1]);
        }

        [TestMethod]
        public void TestAddition()
        {
            double[,] array1 = { { 1, 2 }, { 3, 4 } };
            double[,] array2 = { { 5, 6 }, { 7, 8 } };
            var tensor1 = new QuaternionTensor(array1);
            var tensor2 = new QuaternionTensor(array2);
            var result = tensor1 + tensor2;

            double[,] expected = { { 6, 8 }, { 10, 12 } };
            CollectionAssert.AreEqual(expected, result.Tensor);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            double[,] array1 = { { 5, 6 }, { 7, 8 } };
            double[,] array2 = { { 1, 2 }, { 3, 4 } };
            var tensor1 = new QuaternionTensor(array1);
            var tensor2 = new QuaternionTensor(array2);
            var result = tensor1 - tensor2;

            double[,] expected = { { 4, 4 }, { 4, 4 } };
            CollectionAssert.AreEqual(expected, result.Tensor);
        }

        [TestMethod]
        public void TestMultiplicationByScalar()
        {
            double[,] array = { { 1, 2 }, { 3, 4 } };
            var tensor = new QuaternionTensor(array);
            var result = tensor * 2.0;

            double[,] expected = { { 2, 4 }, { 6, 8 } };
            CollectionAssert.AreEqual(expected, result.Tensor);
        }

        [TestMethod]
        public void TestDivisionByScalar()
        {
            double[,] array = { { 2, 4 }, { 6, 8 } };
            var tensor = new QuaternionTensor(array);
            var result = tensor / 2.0;

            double[,] expected = { { 1, 2 }, { 3, 4 } };
            CollectionAssert.AreEqual(expected, result.Tensor);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivisionByZero()
        {
            double[,] array = { { 1, 2 }, { 3, 4 } };
            var tensor = new QuaternionTensor(array);
            var result = tensor / 0.0;
        }
    }
}