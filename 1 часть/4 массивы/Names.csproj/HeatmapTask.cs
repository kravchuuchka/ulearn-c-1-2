using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            string[] days = new string[30];
            InitializeArray(days, 2);

            string[] months = new string[12];
            InitializeArray(months, 1);

            double[,] daysAndMonths = new double[days.Length, months.Length];
            foreach (var e in names)
            {
                if (e.BirthDate.Day != 1)
                    daysAndMonths[e.BirthDate.Day - 2, e.BirthDate.Month - 1]++;
            }
            return new HeatmapData("Пример карты интенсивностей", daysAndMonths, days, months);
        }

        public static string[] InitializeArray(string[] array, int shiftNumbers)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = (i + shiftNumbers).ToString();
            return array;
        }
    }
}
