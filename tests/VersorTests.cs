using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuaternionObjects;
using System.Drawing;

namespace CircuitCalculator.Tests
{
    [TestClass]
    public class VersorTests
    {
        [TestMethod]
        public void TestConstructorWithCoordinates()
        {
            var versor = new Versor(true, 1.0, 2.0, 3.0);
            Assert.AreEqual(true, versor.Right);
            Assert.AreEqual(1.0, versor.X);
            Assert.AreEqual(2.0, versor.Y);
            Assert.AreEqual(3.0, versor.Z);
        }

        [TestMethod]
        public void TestConstructorWithVector()
        {
            var vector = new Vector(1.0, 2.0, 3.0);
            var versor = new Versor(vector, false);
            Assert.AreEqual(false, versor.Right);
            Assert.AreEqual(1.0, versor.X);
            Assert.AreEqual(2.0, versor.Y);
            Assert.AreEqual(3.0, versor.Z);
        }

        [TestMethod]
        public void TestConstructorWithQuaternion()
        {
            var quaternion = new Quaternion(1.0, 2.0, 3.0, 4.0);
            var versor = new Versor(quaternion, true);
            Assert.AreEqual(true, versor.Right);
            Assert.AreEqual(2.0, versor.X);
            Assert.AreEqual(3.0, versor.Y);
            Assert.AreEqual(4.0, versor.Z);
        }

        [TestMethod]
        public void TestCopy()
        {
            var versor = new Versor(true, 1.0, 2.0, 3.0);
            var copy = versor.Copy();
            Assert.AreEqual(versor.Right, copy.Right);
            Assert.AreEqual(versor.X, copy.X);
            Assert.AreEqual(versor.Y, copy.Y);
            Assert.AreEqual(versor.Z, copy.Z);
        }

        [TestMethod]
        public void TestToVector()
        {
            var versor = new Versor(true, 1.0, 2.0, 3.0);
            var vector = versor.ToVector();
            Assert.AreEqual(versor.X, vector.X);
            Assert.AreEqual(versor.Y, vector.Y);
            Assert.AreEqual(versor.Z, vector.Z);
        }

        [TestMethod]
        public void TestOffset()
        {
            var versor = new Versor(true, 1.0, 2.0, 3.0);
            versor.Offset(1.0, 1.0, 1.0);
            Assert.AreEqual(2.0, versor.X);
            Assert.AreEqual(3.0, versor.Y);
            Assert.AreEqual(4.0, versor.Z);
        }

        [TestMethod]
        public void TestStaticOffset()
        {
            var versor1 = new Versor(true, 1.0, 2.0, 3.0);
            var versor2 = new Versor(true, 4.0, 5.0, 6.0);
            Versor[] versors = { versor1, versor2 };
            Versor.Offset(versors, 1.0, 1.0, 1.0);
            Assert.AreEqual(2.0, versor1.X);
            Assert.AreEqual(3.0, versor1.Y);
            Assert.AreEqual(4.0, versor1.Z);
            Assert.AreEqual(5.0, versor2.X);
            Assert.AreEqual(6.0, versor2.Y);
            Assert.AreEqual(7.0, versor2.Z);
        }

        [TestMethod]
        public void TestGetProjectedPoint()
        {
            var versor = new Versor(true, 1.0, 2.0, 3.0);
            var point = versor.GetProjectedPoint(2.0);
            Assert.AreEqual(0.5f, point.X);
            Assert.AreEqual(1.0f, point.Y);
        }

        [TestMethod]
        public void TestProject()
        {
            var versor1 = new Versor(true, 1.0, 2.0, 3.0);
            var versor2 = new Versor(true, 4.0, 5.0, 6.0);
            Versor[] versors = { versor1, versor2 };
            var points = Versor.Project(versors, 2.0);
            Assert.AreEqual(0.5f, points[0].X);
            Assert.AreEqual(1.0f, points[0].Y);
            Assert.AreEqual(1.0f, points[1].X);
            Assert.AreEqual(1.25f, points[1].Y);
        }
    }
}