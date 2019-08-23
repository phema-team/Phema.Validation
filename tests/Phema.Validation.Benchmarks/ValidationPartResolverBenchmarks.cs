using System.Runtime.Serialization;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Benchmarks
{
	public class ValidationPartResolverBenchmarks
	{
		private IValidationContext defaultValidationContext;
		private IValidationContext dataMemberValidationContext;
		private IValidationContext pascalCaseValidationContext;
		private IValidationContext camelCaseValidationContext;

		private TestModel model;

		[GlobalSetup]
		public void GlobalSetup()
		{
			defaultValidationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();

			dataMemberValidationContext = new ServiceCollection()
				.AddValidation(o => o.ValidationPartResolver = ValidationPartResolvers.DataMember)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();

			pascalCaseValidationContext = new ServiceCollection()
				.AddValidation(o => o.ValidationPartResolver = ValidationPartResolvers.PascalCase)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();

			camelCaseValidationContext = new ServiceCollection()
				.AddValidation(o => o.ValidationPartResolver = ValidationPartResolvers.CamelCase)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
			
			model = new TestModel();
		}

		[Benchmark]
		public void Default()
		{
			defaultValidationContext.When(model, m => m.Default).AddError("Error");
		}
		
		[Benchmark]
		public void DataMember()
		{
			dataMemberValidationContext.When(model, m => m.DataMember).AddError("Error");
		}
		
		[Benchmark]
		public void PascalCase()
		{
			pascalCaseValidationContext.When(model, m => m.pascalCase).AddError("Error");
		}
		
		[Benchmark]
		public void CamelCase()
		{
			camelCaseValidationContext.When(model, m => m.CamelCase).AddError("Error");
		}

		private class TestModel
		{
			public int Default { get; set; }
			
			[DataMember(Name = "dataMember")]
			public int DataMember { get; set; }
			
			// ReSharper disable once InconsistentNaming
			public int pascalCase { get; set; }
			
			public int CamelCase { get; set; }
		}
	}
}