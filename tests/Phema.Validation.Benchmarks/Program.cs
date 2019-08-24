using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

namespace Phema.Validation.Benchmarks
{
	public class Program
	{
		private static void Main(string[] args) => 
			BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
				.Run(args, new ManualConfig()
					.With(ConsoleLogger.Default)
					.With(Job.Core
						.WithInvocationCount(1024)
						.WithWarmupCount(100)
						.WithIterationCount(32))
					.With(
						TargetMethodColumn.Method,
						StatisticColumn.Mean,
						StatisticColumn.Error,
						StatisticColumn.Max,
						StatisticColumn.StdDev));
	}
}