using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var sum = 0.0;
			var pointsInWindow = new Queue<DataPoint>();

			foreach (var dataPoint in data)
            {
				sum += dataPoint.OriginalY;
				pointsInWindow.Enqueue(dataPoint);

				if (windowWidth < pointsInWindow.Count)
					sum -= pointsInWindow.Dequeue().OriginalY;

				yield return dataPoint.WithAvgSmoothedY(sum / pointsInWindow.Count);
            }
		}
	}
}