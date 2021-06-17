using System;
using NUnit.Framework;
using static Manipulation.Manipulator;
using static System.Math;
using static Manipulation.TriangleTask;


namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var wristX = x - Cos(alpha) * Palm;
            var wristY = y + Sin(alpha) * Palm;
            var distanceToWrist = Sqrt(wristX * wristX + wristY * wristY);
            var elbow = GetABAngle(UpperArm, Forearm, distanceToWrist);
            var shoulder = GetABAngle(UpperArm, distanceToWrist, Forearm) + Atan2(wristY, wristX);
            var wrist = -alpha - shoulder - elbow;

            return new[] { shoulder, elbow, wrist };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        public const double FullSize = UpperArm + Forearm + Palm;
        public const int TestCount = 1111;

        [Test]
        public void TestMoveManipulatorTo()
        {
            var random = new Random(15000);
            for (int i = 0; i < TestCount; i++)
            {
                var x = random.NextDouble() * FullSize;
                var y = random.NextDouble() * FullSize;
                var alpha = random.NextDouble() * 2 * PI;
                var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
                Assert.AreEqual(3, angles.Length);
                if (Double.IsNaN(angles[0]))
                    Assert.AreEqual(new[] { double.NaN, double.NaN, double.NaN }, angles);
                else
                {
                    var joints = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                    Assert.AreEqual(3, joints.Length);
                    Assert.AreEqual(joints[2].X, x, 0.0001);
                    Assert.AreEqual(joints[2].Y, y, 0.0001);
                }
            }
        }
    }
}