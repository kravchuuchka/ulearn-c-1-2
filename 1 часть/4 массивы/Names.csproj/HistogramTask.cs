using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            double[] dayNumber = new double[32];
            string[] dayNumberStr = new string[31];
            foreach (var e in names)
            {
                if (e.Name == name && e.BirthDate.Day != 1)
                    dayNumber[e.BirthDate.Day]++;
            }
            ConvertToString(dayNumberStr);
            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name),
            dayNumberStr,
            dayNumber.Skip(1).ToArray());
        }

        public static string[] ConvertToString(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = (i + 1).ToString();
            return array;
        }
    }
}
