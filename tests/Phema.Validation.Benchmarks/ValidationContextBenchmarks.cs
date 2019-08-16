using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Benchmarks
{
	public class ValidationContextBenchmarks
	{
		private IValidationContext validationContext;

		[IterationSetup]
		public void IterationSetup()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Benchmark]
		public void Simple()
		{
			validationContext.When("Model", "Value").AddError("Error");
		}

		[Benchmark]
		public void CreateScope()
		{
			validationContext.CreateScope("Model");
		}

		[Benchmark]
		public void IsValid()
		{
			validationContext.IsValid("Model");
		}

		[Benchmark]
		public void EnsureIsValid()
		{
			validationContext.EnsureIsValid("Model");
		}
	}
}