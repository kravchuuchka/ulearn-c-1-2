using System;
using NUnit.Framework;
using static System.Math;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if ((a < 0 || b < 0 || c < 0) || (a + b < c || a + c < b || b + c < a))
                return double.NaN;
            var angle = Acos((a * a + b * b - c * c) / (2 * a * b));
            return angle;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, PI / 2)]
        [TestCase(1, 1, 1, PI / 3)]
        [TestCase(-5, 3, 0, double.NaN)]
        [TestCase(-1, -2, -3, double.NaN)]
        [TestCase(8, 1, 0, double.NaN)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var receivedAngle = TriangleTask.GetABAngle(a, b, c);
            if (expectedAngle == double.NaN || receivedAngle == double.NaN)
                Assert.AreEqual(receivedAngle, expectedAngle);
            else 
                Assert.AreEqual(receivedAngle, expectedAngle, 1e-5);
        }
    }
}