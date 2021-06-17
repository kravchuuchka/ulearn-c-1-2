using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			double previous = double.NaN;

			foreach (var dataPoint in data)
            {
				if (previous.Equals(obj:double.NaN))
                {
					previous = dataPoint.OriginalY;
					yield return dataPoint.WithExpSmoothedY(previous);
					continue;
                }

				previous = alpha * dataPoint.OriginalY + (1 - alpha) * previous;

				yield return dataPoint.WithExpSmoothedY(previous);
            }

		}
	}
}