using System;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Benchmarks
{
	public class ValidationContextExpressionBenchmarks
	{
		private TestModel model;
		private IServiceProvider serviceProvider;
		private IValidationContext validationContext;

		[GlobalSetup]
		public void GlobalSetup()
		{
			model = new TestModel
			{
				Model = new TestModel
				{
					Array = new[]
					{
						new TestModel
						{
							Model = new TestModel()
						},
						new TestModel()
					}
				}
			};

			serviceProvider = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider();
		}

		[IterationSetup]
		public void IterationSetup()
		{
			validationContext = serviceProvider.CreateScope()
				.ServiceProvider
				.GetRequiredService<IValidationContext>();
		}

		[Benchmark]
		public void SimpleExpression()
		{
			validationContext.When(model, m => m.Model)
				.AddError("Property is 12");
		}

		[Benchmark]
		public void SimpleExpression_CompiledValue()
		{
			validationContext.When(model, m => m.Model)
				.Is(value => true)
				.AddError("Property is 12");
		}

		[Benchmark]
		public void ChainedExpression()
		{
			validationContext.When(model, m => m.Model.Model)
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedExpression_CompiledValue()
		{
			validationContext.When(model, m => m.Model.Model)
				.Is(value => true)
				.AddError("Error");
		}

		[Benchmark]
		public void ArrayAccessExpression()
		{
			validationContext.When(model, m => m.Model.Array[0])
				.AddError("Error");
		}

		[Benchmark]
		public void ArrayAccessExpression_CompiledValue()
		{
			validationContext.When(model, m => m.Model.Array[0])
				.Is(value => true)
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedArrayAccessExpression()
		{
			validationContext.When(model, m => m.Model.Array[0].Model)
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedArrayAccessExpression_CompiledValue()
		{
			validationContext.When(model, m => m.Model.Array[0].Model)
				.Is(value => true)
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedArrayAccess()
		{
			var provider = new {ForModel = new {Index = 0}};

			validationContext.When(model, m => m.Model.Array[provider.ForModel.Index].Model)
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedArrayAccess_CompiledValue()
		{
			var provider = new {ForModel = new {Index = 0}};

			validationContext.When(model, m => m.Model.Array[provider.ForModel.Index].Model)
				.Is(value => true)
				.AddError("Error");
		}

		[Benchmark]
		public void CreateScope_SimpleExpression()
		{
			validationContext.CreateScope(model, m => m.Model);
		}

		[Benchmark]
		public void CreateScope_ChainedExpression()
		{
			validationContext.CreateScope(model, m => m.Model.Model);
		}

		[Benchmark]
		public void IsValid_Empty()
		{
			validationContext.IsValid();
		}

		[Benchmark]
		public void IsValid_Expression()
		{
			validationContext.IsValid(model, m => m.Model);
		}

		[Benchmark]
		public void EnsureIsValid_Expression()
		{
			validationContext.EnsureIsValid(model, m => m.Model);
		}

		private class TestModel
		{
			public TestModel[] Array { get; set; }
			public TestModel Model { get; set; }
		}
	}
}