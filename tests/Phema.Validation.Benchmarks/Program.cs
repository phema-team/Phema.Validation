using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Phema.Validation.Benchmarks
{
	public class Program
	{
		private static void Main(string[] args) =>
			BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
				.Run(args, DefaultConfig.Instance
					.With(Job.Core
						.WithInvocationCount(32)
						.WithWarmupCount(100)
						.WithIterationCount(10000))
					.With(MemoryDiagnoser.Default));
	}
}