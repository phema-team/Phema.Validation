using System;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Benchmarks
{
	public class ValidationContextBenchmarks
	{
		private IServiceProvider serviceProvider;
		private IValidationContext validationContext;

		[GlobalSetup]
		public void GlobalSetup()
		{
			serviceProvider = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider();
		}

		[IterationSetup]
		public void IterationSetup()
		{
			validationContext = serviceProvider
				.CreateScope()
				.ServiceProvider
				.GetRequiredService<IValidationContext>();
		}

		[Benchmark]
		public void Simple()
		{
			validationContext.When("Model", "Value").AddValidationError("Error");
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