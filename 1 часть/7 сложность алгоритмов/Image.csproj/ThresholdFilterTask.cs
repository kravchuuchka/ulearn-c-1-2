using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
            var pixels = new List <double>();
            var length = original.GetLength(0);
            var width = original.GetLength(1);
            var result = new double[length, width];

            whitePixelsFraction = GetThreshold(original, pixels, whitePixelsFraction);

            for (var y = 0; y < width; y++)
                for (var x = 0; x < length; x++)
                    if (original[x, y] >= whitePixelsFraction)
                        result[x, y] = 1;
                    else
                        result[x, y] = 0;
            return result;
        }

        private static double GetThreshold(double[,] original, List<double> pixels, double whitePixelsFraction)
        {
            foreach (var e in original)
                pixels.Add(e);
            pixels.Sort();
            var count = pixels.Count;
            whitePixelsFraction = (int)(whitePixelsFraction * count);

            if (whitePixelsFraction > 0 && whitePixelsFraction <= count)
                whitePixelsFraction = pixels[(int)(count - whitePixelsFraction)];
            else if (whitePixelsFraction > count)
                whitePixelsFraction = int.MaxValue;
            else
                whitePixelsFraction = int.MaxValue;

            return whitePixelsFraction;
        }
	}
}