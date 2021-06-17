using System;
using System.Drawing;
using NUnit.Framework;
using static System.Math;
using static Manipulation.Manipulator;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            float positionX = UpperArm * (float)Cos(shoulder);
            float positionY = UpperArm * (float)Sin(shoulder);
            PointF elbowPos = new PointF(positionX, positionY);
            positionX += GetXOffset(Forearm, elbow, shoulder, 0, 1);
            positionY += GetYOffset(Forearm, elbow, shoulder, 0, 1);
            PointF wristPos = new PointF(positionX, positionY);
            positionX += GetXOffset(Palm, elbow, shoulder, wrist, 2);
            positionY += GetYOffset(Palm, elbow, shoulder, wrist, 2);
            PointF palmEndPos = new PointF(positionX, positionY);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        public static float GetXOffset(float side, double elbow, double shoulder, double wrist, int coefficient)
        {
            var angle = elbow + shoulder + wrist - coefficient * PI;
            return side * (float)Cos(angle);
        }

        public static float GetYOffset(float side, double elbow, double shoulder, double wrist, int coefficient)
        {
            var angle = elbow + shoulder + wrist - coefficient * PI;
            return side * (float)Sin(angle);
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(PI / 2, PI / 2, PI, Forearm + Palm, UpperArm)]
        [TestCase(PI / 2, PI, 3 * PI, 0, Forearm + UpperArm + Palm)]
        [TestCase(PI / 2, PI / 2, PI / 2, Forearm, UpperArm - Palm)]
        [TestCase(PI / 2, 3 * PI / 2, 3 * PI / 2, -Forearm, UpperArm - Palm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(GetDistance(joints[0], joints[1]), Forearm);
            Assert.AreEqual(GetDistance(joints[1], joints[2]), Palm);
            Assert.AreEqual(GetDistance(joints[0], new PointF(0, 0)), UpperArm);
        }

        private static double GetDistance(PointF firstPoint, PointF secondPoint)
        {
            var differenceX = (firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X);
            var differenceY = (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y);

            return Sqrt(differenceX + differenceY);
        }
    }
}