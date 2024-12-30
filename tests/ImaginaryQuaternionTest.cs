using System.Numerics;
using Equations.Expressions;

namespace Calculator.Tests
{
    [TestClass]
    public class ImaginaryQuaternionTest
    {
        [TestMethod]
        public void TestAddition()
        {
            var a = new ImaginaryQuaternion(new Complex(1, 2), new Complex(3, 4), new Complex(5, 6));
            var b = new ImaginaryQuaternion(new Complex(7, 8), new Complex(9, 10), new Complex(11, 12));
            var result = a + b;

            Console.WriteLine($"Addition Result: I={result.I}, J={result.J}, K={result.K}");

            Assert.AreEqual(new Complex(8, 10), result.I);
            Assert.AreEqual(new Complex(12, 14), result.J);
            Assert.AreEqual(new Complex(16, 18), result.K);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            var a = new ImaginaryQuaternion(new Complex(7, 8), new Complex(9, 10), new Complex(11, 12));
            var b = new ImaginaryQuaternion(new Complex(1, 2), new Complex(3, 4), new Complex(5, 6));
            var result = a - b;

            Console.WriteLine($"Subtraction Result: I={result.I}, J={result.J}, K={result.K}");

            Assert.AreEqual(new Complex(6, 6), result.I);
            Assert.AreEqual(new Complex(6, 6), result.J);
            Assert.AreEqual(new Complex(6, 6), result.K);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            var a = new ImaginaryQuaternion(new Complex(1, 2), new Complex(3, 4), new Complex(5, 6));
            var b = new ImaginaryQuaternion(new Complex(7, 8), new Complex(9, 10), new Complex(11, 12));
            var result = a * b;

            Console.WriteLine($"Multiplication Result: I={result.I}, J={result.J}, K={result.K}");

            Assert.AreEqual(new Complex(-9, 22), result.I);
            Assert.AreEqual(new Complex(-13, 66), result.J);
            Assert.AreEqual(new Complex(-17, 132), result.K);
        }

        [TestMethod]
        public void TestDivision()
        {
            var a = new ImaginaryQuaternion(new Complex(7, 8), new Complex(9, 10), new Complex(11, 12));
            var b = new ImaginaryQuaternion(new Complex(1, 2), new Complex(3, 4), new Complex(5, 6));
            var result = a / b;

            Console.WriteLine($"Division Result: I={result.I}, J={result.J}, K={result.K}");

            Assert.AreEqual(new Complex(3.6, -0.2), result.I);
            Assert.AreEqual(new Complex(2.4, 0.8), result.J);
            Assert.AreEqual(new Complex(2.2, 0.4), result.K);
        }

        [TestMethod]
        public void TestToString()
        {
            var a = new ImaginaryQuaternion(new Complex(1, 2), new Complex(3, 4), new Complex(5, 6));
            var result = a.ToString();

            Console.WriteLine($"ToString Result: {result}");

            Assert.AreEqual("(1 + 2i, 3 + 4i, 5 + 6i)", result);
        }
    }
}