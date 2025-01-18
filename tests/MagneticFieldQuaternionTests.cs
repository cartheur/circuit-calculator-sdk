using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuaternionObjects;

namespace CircuitCalculator.Tests
{
    [TestClass]
    public class MagneticFieldQuaternionTests
    {
        [TestMethod]
        public void TestConstructorWithCoordinates()
        {
            var magneticField = new MagneticFieldQuaternion(0, 1.0, 2.0, 3.0);
            Assert.AreEqual(0, magneticField.W);
            Assert.AreEqual(1.0, magneticField.X);
            Assert.AreEqual(2.0, magneticField.Y);
            Assert.AreEqual(3.0, magneticField.Z);
        }

        [TestMethod]
        public void TestConstructorWithVector()
        {
            var vector = new Vector(1.0, 2.0, 3.0);
            var magneticField = new MagneticFieldQuaternion(vector);
            Assert.AreEqual(0, magneticField.W);
            Assert.AreEqual(1.0, magneticField.X);
            Assert.AreEqual(2.0, magneticField.Y);
            Assert.AreEqual(3.0, magneticField.Z);
        }

        [TestMethod]
        public void TestNormalize()
        {
            var magneticField = new MagneticFieldQuaternion(0, 1.0, 2.0, 2.0);
            var normalized = magneticField.Normalize();
            double norm = Math.Sqrt(1.0 + 4.0 + 4.0);
            Assert.AreEqual(0, normalized.W);
            Assert.AreEqual(1.0 / norm, normalized.X);
            Assert.AreEqual(2.0 / norm, normalized.Y);
            Assert.AreEqual(2.0 / norm, normalized.Z);
        }

        [TestMethod]
        public void TestAddition()
        {
            var magneticField1 = new MagneticFieldQuaternion(0, 1.0, 2.0, 3.0);
            var magneticField2 = new MagneticFieldQuaternion(0, 4.0, 5.0, 6.0);
            var result = magneticField1 + magneticField2;
            Assert.AreEqual(0, result.W);
            Assert.AreEqual(5.0, result.X);
            Assert.AreEqual(7.0, result.Y);
            Assert.AreEqual(9.0, result.Z);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            var magneticField1 = new MagneticFieldQuaternion(0, 4.0, 5.0, 6.0);
            var magneticField2 = new MagneticFieldQuaternion(0, 1.0, 2.0, 3.0);
            var result = magneticField1 - magneticField2;
            Assert.AreEqual(0, result.W);
            Assert.AreEqual(3.0, result.X);
            Assert.AreEqual(3.0, result.Y);
            Assert.AreEqual(3.0, result.Z);
        }

        [TestMethod]
        public void TestMultiplicationByScalar()
        {
            var magneticField = new MagneticFieldQuaternion(0, 1.0, 2.0, 3.0);
            var result = magneticField * 2.0;
            Assert.AreEqual(0, result.W);
            Assert.AreEqual(2.0, result.X);
            Assert.AreEqual(4.0, result.Y);
            Assert.AreEqual(6.0, result.Z);
        }

        [TestMethod]
        public void TestDivisionByScalar()
        {
            var magneticField = new MagneticFieldQuaternion(0, 2.0, 4.0, 6.0);
            var result = magneticField / 2.0;
            Assert.AreEqual(0, result.W);
            Assert.AreEqual(1.0, result.X);
            Assert.AreEqual(2.0, result.Y);
            Assert.AreEqual(3.0, result.Z);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestDivisionByZero()
        {
            var magneticField = new MagneticFieldQuaternion(0, 1.0, 2.0, 3.0);
            var result = magneticField / 0.0;
        }

        [TestMethod]
        public void TestToVector()
        {
            var magneticField = new MagneticFieldQuaternion(0, 1.0, 2.0, 3.0);
            var vector = magneticField.ToVector();
            Assert.AreEqual(1.0, vector.X);
            Assert.AreEqual(2.0, vector.Y);
            Assert.AreEqual(3.0, vector.Z);
        }
    }
}