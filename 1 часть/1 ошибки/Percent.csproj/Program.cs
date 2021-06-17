using System;

namespace Percent
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();
            double userOutput = Calculate(userInput);

            Console.WriteLine(userOutput);
        }

        public static double Calculate(string userInput)
        {
            var values = userInput.Split();
            var sum = Convert.ToDouble(values[0]);
            var rate = Convert.ToDouble(values[1]);
            var months = int.Parse(values[2]);

            return sum * Math.Pow(1 + rate / 12 / 100, months);
        }
    }
}
