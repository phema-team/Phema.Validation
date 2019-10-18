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
						.WithInvocationCount(2048)
						.WithWarmupCount(200)
						.WithIterationCount(1000))
					.With(MemoryDiagnoser.Default));
	}
}