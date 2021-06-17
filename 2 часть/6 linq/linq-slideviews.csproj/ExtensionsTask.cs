using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		public static double Median(this IEnumerable<double> items)
		{
			var list = items.ToList();
			if (list.Count == 0)
				throw new InvalidOperationException();
			list.Sort();
			if (list.Count % 2 == 1)
				return list[list.Count / 2];
			return (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2;
		}

		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var prev = default(T);
			var isFirstIteration = true;
			foreach (var e in items)
			{
				if (!isFirstIteration)
					yield return Tuple.Create(prev, e);
				isFirstIteration = false;
				prev = e;
			}
		}
	}
}