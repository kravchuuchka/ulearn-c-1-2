using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public Vector()
        {
            X = 0;
            Y = 0;
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(vector, this);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }

        public static double GetLength(Segment segment)
        {
            var vector = new Vector();
            vector.X = segment.Begin.X - segment.End.X;
            vector.Y = segment.Begin.Y - segment.End.Y;
            return GetLength(vector);
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var segmentLength = Geometry.GetLength(segment);
            var length1 = GetLength(new Vector() { X = vector.X - segment.Begin.X, Y = vector.Y - segment.Begin.Y });
            var length2 = GetLength(new Vector() { X = vector.X - segment.End.X, Y = vector.Y - segment.End.Y });

            return IsAlmostEqual(length1 + length2, segmentLength);
        }

        public static bool IsAlmostEqual(double length1, double length2)
        {
            const double epsilon = 0.00000001;
            return Math.Abs(length1 - length2) < epsilon;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public static double GetLength(Vector vector)
        {
            return Geometry.GetLength(vector);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}
