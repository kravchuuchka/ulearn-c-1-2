using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            task.Run();
            var time = new Stopwatch();
            time.Start();
            for (var repeat = repetitionCount; repeat > 0; repeat--)
                task.Run();
            time.Stop();
            return time.Elapsed.TotalMilliseconds / repetitionCount;
        }
	}

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        private const int GeneralCount = 10000;

        private class Builder : ITask
        {
            public void Run()
            {
                var stringByBuilder = new StringBuilder();
                for (var i = 0; i < GeneralCount; i++)
                    stringByBuilder.Append("a");
                stringByBuilder.ToString();
            }
        }

        private class Constructor : ITask
        {
            public void Run()
            {
                var stringByConstructor = new string('a', GeneralCount);
            }
        }

        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var test = new Benchmark();
            var stringByBuilder = test.MeasureDurationInMs(new Builder(), GeneralCount);
            var stringByConstructor = test.MeasureDurationInMs(new Constructor(), GeneralCount);
            Assert.Less(stringByConstructor, stringByBuilder);
        }
    }
}