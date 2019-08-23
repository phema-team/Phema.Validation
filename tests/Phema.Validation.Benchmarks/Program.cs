using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace Phema.Validation.Benchmarks
{
	public class Program
	{
		public static void Main()
		{
			var config = new ManualConfig()
				.With(ConsoleLogger.Default)
				.With(Job.Core
					.WithInvocationCount(16)
					.WithWarmupCount(100)
					.WithIterationCount(1000))
				.With(
					TargetMethodColumn.Method,
					StatisticColumn.Mean,
					StatisticColumn.Max,
					StatisticColumn.Iterations,
					StatisticColumn.Error,
					StatisticColumn.StdDev);

			BenchmarkRunner.Run<ValidationContextBenchmarks>(config);
			BenchmarkRunner.Run<ValidationContextExpressionBenchmarks>(config);
			BenchmarkRunner.Run<ValidationPartResolverBenchmarks>(config);
		}
	}
}