using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Benchmarks
{
	public class ValidationContextExpressionBenchmarks
	{
		private TestModel model;
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
		}

		[IterationSetup]
		public void IterationSetup()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Benchmark]
		public void SimpleExpression()
		{
			validationContext.When(model, m => m.Model)
				.AddError("Property is 12");
		}

		[Benchmark]
		public void ChainedExpression()
		{
			validationContext.When(model, m => m.Model.Model)
				.AddError("Error");
		}

		[Benchmark]
		public void ArrayAccessExpression()
		{
			validationContext.When(model, m => m.Model.Array[0])
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedArrayAccessExpression()
		{
			validationContext.When(model, m => m.Model.Array[0].Model)
				.AddError("Error");
		}

		[Benchmark]
		public void ChainedArrayAccess_DynamicInvoke_Expression()
		{
			var provider = new {ForArray = new {Index = 0}};

			validationContext.When(model, m => m.Model.Array[provider.ForArray.Index].Model)
				.AddError("Error");
		}

		[Benchmark]
		public void CreateFor_SimpleExpression()
		{
			validationContext.CreateFor(model, m => m.Model);
		}

		[Benchmark]
		public void CreateFor_ChainedExpression()
		{
			validationContext.CreateFor(model, m => m.Model.Model);
		}

		[Benchmark]
		public void IsValid_Empty()
		{
			validationContext.IsValid(model, m => m.Model);
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