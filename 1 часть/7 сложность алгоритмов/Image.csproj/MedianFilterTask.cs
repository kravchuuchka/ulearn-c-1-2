using System.Collections.Generic;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
			var length = original.GetLength(0);
			var width = original.GetLength(1);
			var result = new double[length, width];
			for (int y = 0; y < width; y++)
				for (int x = 0; x < length; x++)
                {
					var pixels = new List<double>();
					int xFrom = GetCoordinateFrom(x);
					int yFrom = GetCoordinateFrom(y);
					int xTo = GetCoordinateTo(x, length);
					int yTo = GetCoordinateTo(y, width);
					
					for (int i = xFrom; i <= xTo; i++)
						for (int j = yFrom; j <= yTo; j++)
							pixels.Add(original[i, j]);
                
					pixels.Sort();
					result[x, y] = GetMedian(pixels);
				}
			return result;
		}

		private static int GetCoordinateFrom(int coordinate)
        {
			return coordinate > 0 ? coordinate - 1 : 0;
		}

		private static int GetCoordinateTo(int coordinate, int size)
        {
			return coordinate < size - 1 ? coordinate + 1 : size - 1;
		}

		private static double GetMedian(List<double> window)
        {
			var count = window.Count;
			return count % 2 == 0 ? (window[count / 2] + window[count / 2 - 1]) / 2 : window[count / 2];
        }
	}
}