using System.Collections.Generic;

namespace StructBenchmarking
{
    public interface IBuildChartData
    {
        ITask CreateByClass(int size);

        ITask CreateByStruct(int size);
    }

    public class ArrayTask : IBuildChartData
    {
        public ITask CreateByClass(int size) => new ClassArrayCreationTask(size);

        public ITask CreateByStruct(int size) => new StructArrayCreationTask(size);
    }

    public class ArgumentTask : IBuildChartData
    {
        public ITask CreateByClass(int size) => new MethodCallWithClassArgumentTask(size);

        public ITask CreateByStruct(int size) => new MethodCallWithStructArgumentTask(size);
    }

    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount) =>
            BuildChartData(benchmark, repetitionsCount, new ArrayTask(), "Create array");

        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount) =>
            BuildChartData(benchmark, repetitionsCount, new ArgumentTask(), "Call method with argument");

        private static ChartData BuildChartData(
            IBenchmark benchmark, int repetitionsCount, IBuildChartData task, string title)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            foreach (var size in Constants.FieldCounts)
            {
                var classTime = benchmark.MeasureDurationInMs(task.CreateByClass(size), repetitionsCount);
                var structTime = benchmark.MeasureDurationInMs(task.CreateByStruct(size), repetitionsCount);
                classesTimes.Add(new ExperimentResult(size, classTime));
                structuresTimes.Add(new ExperimentResult(size, structTime));
            }
            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes
            };
        }
    }
}