using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			// a-точка с координатами (ax,ay), b-точка с координатами (bx,by) c-точка с координатами (x,y), ab-исходный отрезок
			if ((ax == x && ay == y) || (bx == x && by == y)) return 0;

			double ab = GetLength(ax, ay, bx, by);
			double ac = GetLength(ax, ay, x, y);

			if (ab == 0) return ac;

			double bc = GetLength(bx, by, x, y);

			if (IsAngleObtuse(bc, ab, ac)) return bc;
			if (IsAngleObtuse(ac, ab, bc)) return ac;

			return 2 * GetHeronsFormula(ab, bc, ac) / ab;
		}

		public static double GetHeronsFormula(double ab, double bc, double ac)
        {
			double p = (ac + bc + ab) / 2;
			return Math.Sqrt(p * (p - ab) * (p - bc) * (p - ac));
		}

		public static double GetLength(double x1, double y1, double x2, double y2)
		{
			return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
		}

		public static bool IsAngleObtuse(double a, double b, double c)
		{
			// по теореме косинусов a и b-стороны, образующие искомый угол, c-противолежащая сторона угла
			return (a * a + b * b - c * c) / (2 * a * b) < 0;
		}
	}
}