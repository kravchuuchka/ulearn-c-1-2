using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			var times = visits.GroupBy(x => x.UserId).SelectMany(z =>
				z.OrderBy(t => t.DateTime).Bigrams().Where(y => y.Item1.SlideType == slideType)
					.Select(x => x.Item2.DateTime - x.Item1.DateTime)
                    .Where(x => x >= TimeSpan.FromMinutes(1) && x <= TimeSpan.FromHours(2))
					.Select(x => x.TotalMinutes).ToList());
			if (times.Count() == 0)
				return 0;
			return times.Median();
		}
	}
}