using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var possibleMaxValues = new LinkedList<double>();
			var pointsInWindow = new Queue<double>();

			foreach (var dataPoint in data)
            {
				pointsInWindow.Enqueue(dataPoint.OriginalY);

				if (pointsInWindow.Count > windowWidth &&
				Math.Abs(possibleMaxValues.First.Value - pointsInWindow.Dequeue()) < 0.000001)
					possibleMaxValues.RemoveFirst();

				while (possibleMaxValues.Count > 0 && 
			    possibleMaxValues.Last.Value < dataPoint.OriginalY)
					possibleMaxValues.RemoveLast();

				possibleMaxValues.AddLast(dataPoint.OriginalY);

				yield return dataPoint.WithMaxY(possibleMaxValues.First.Value);
            }
		}
	}
}