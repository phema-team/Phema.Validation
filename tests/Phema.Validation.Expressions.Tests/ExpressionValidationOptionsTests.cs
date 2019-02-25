using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ExpressionValidationOptionsTests
	{
		[Fact]
		public void CustomSeparator()
		{
			var validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModel, TestModelValidationComponent>(),
					expressions: o => o.Separator = "|")
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();

			var model = new TestModel
			{
				Nested = new TestModel
				{
					String = "works"
				}
			};

			var (key, message) = validationContext.When(model, m => m.Nested.String)
				.Is(value => value == "works")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			Assert.Equal("nested|string", key);
			Assert.Equal("template1", message);
		}
		
		[Fact]
		public void DataContractPrefix()
		{
			var validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModel, TestModelValidationComponent>(),
					expressions: o => o.UseDataContractPrefix = true)
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();

			var model = new TestModel
			{
				Nested = new TestModel
				{
					String = "works"
				}
			};

			var (key, message) = validationContext.When(model, m => m.Nested.String)
				.Is(value => value == "works")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			Assert.Equal("test:nested:string", key);
			Assert.Equal("template1", message);
		}
	}
}