using System;

namespace Fractals
{
    internal static class DragonFractalTask
	{
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			double angle1 = Math.PI / 4;
			double angle2 = 3 * Math.PI / 4;
			double x = 1.0;
			double y = 0.0;
			Random random = new Random(seed);
            for (int i = 0; i < iterationsCount; i++)
            {
                double newX;
                double newY;
                if (random.Next(2) == 0)
                {
                    newX = GetNewX(x, y, angle1);
                    newY = GetNewY(x, y, angle1);
                }
                else
                {
                    newX = GetNewX(x, y, angle2) + 1;
                    newY = GetNewY(x, y, angle2);
                }
                x = newX;
                y = newY;
                pixels.SetPixel(x, y);
            }
        }

        public static double GetNewX(double x, double y, double angle)
        {
            return (x * Math.Cos(angle) - y * Math.Sin(angle)) / Math.Sqrt(2);
        }

        public static double GetNewY(double x, double y, double angle)
        {
            return (x * Math.Sin(angle) + y * Math.Cos(angle)) / Math.Sqrt(2);
        }
    }
}