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
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void ValidationContext_DataContract_Property()
		{
			var model = new TestModel();

			var validationMessage = validationContext.When(model, m => m.Property)
				.Is(value => true)
				.AddError("Error");

			Assert.Equal("property", validationMessage.ValidationKey);
		}

		[Fact]
		public void ValidationContext_DataContract_Array()
		{
			var model = new TestModel
			{
				Array = new[] {12}
			};

			var validationMessage = validationContext.When(model, m => m.Array[0])
				.Is(value => true)
				.AddError("Error");

			Assert.Equal("array[0]", validationMessage.ValidationKey);
		}

		[Fact]
		public void ValidationContext_DataContract_List()
		{
			var model = new TestModel
			{
				List = new List<int> {12}
			};

			var validationMessage = validationContext.When(model, m => m.List[0])
				.Is(value => true)
				.AddError("Error");

			Assert.Equal("list[0]", validationMessage.ValidationKey);
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