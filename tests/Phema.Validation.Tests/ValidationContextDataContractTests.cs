using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationContextDataContractTests
	{
		private readonly IValidationContext validationContext;

		public ValidationContextDataContractTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation(options => 
					options.ValidationPartResolver = ValidationDefaults.DataMemberOrDefaultValidationPartResolver)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void DataContract_Property()
		{
			var model = new TestModel();

			var validationMessage = validationContext.When(model, m => m.Property).AddError("Error");

			Assert.Equal("property", validationMessage.ValidationKey);
		}

		[Fact]
		public void DataContract_Array()
		{
			var model = new TestModel
			{
				Array = new[] {12}
			};

			var validationMessage = validationContext.When(model, m => m.Array[0]).AddError("Error");

			Assert.Equal("array[0]", validationMessage.ValidationKey);
		}

		[Fact]
		public void DataContract_List()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var validationMessage = validationContext.When(model, m => m.List[0]).AddError("Error");

			Assert.Equal("list[0]", validationMessage.ValidationKey);
		}

		[Fact]
		public void Expression()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var withPrefix = validationContext.CreateScope(model, m => m.List);

			var (key, _) = withPrefix.When(model.List, list => list.Count).AddError("Error");

			var validationMessage = Assert.Single(validationContext.ValidationDetails);

			Assert.Equal("list.Count", key);
			Assert.Equal("list.Count", validationMessage.ValidationKey);
		}

		[Fact]
		public void Expression_DoubleInnerCreate()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var withPrefix = validationContext.CreateScope(model, m => m.List);
			withPrefix = withPrefix.CreateScope(model, m => m.List);

			var (key, _) = withPrefix.When(model.List, c => c.Count).AddError("Error");

			Assert.Equal("list.list.Count", key);
		}

		[DataContract]
		private class TestModel
		{
			[DataMember(Name = "property")]
			public int Property { get; set; }

			[DataMember(Name = "array")]
			public int[] Array { get; set; }

			[DataMember(Name = "list")]
			public List<int> List { get; set; }
		}
	}
}