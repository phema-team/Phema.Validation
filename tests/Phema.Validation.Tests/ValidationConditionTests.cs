using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddValidationComponent<TestModel, TestModelValidation, TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}
		
		[Fact]
		public void Is_CompareByValue()
		{
			validationContext.When("key", "value")
				.Is(value => value == "value")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			var (key, message) = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void Is_WithoutValue()
		{
			validationContext.When("key", "value")
				.Is(() => true)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			var (key, message) = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}
		
		[Fact]
		public void Is_CompareCombined_True()
		{
			validationContext.When("key", "value")
				.Is(() => true)
				.Is(value => value == "value")
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			var (key, message) = Assert.Single(validationContext.Errors);
			
			Assert.Equal("key", key);
			Assert.Equal("template1", message);
		}
		
		[Fact]
		public void Is_CompareCombined_SomeFalse()
		{
			validationContext.When("key", "value")
				.Is(() => true)
				.Is(value => false)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			Assert.Empty(validationContext.Errors);
		}

		[Fact]
		public void ConditionsOrder()
		{
			var raises = 0;
			
			validationContext.When("key", "value")
				.Is(() =>
				{
					Assert.Equal(0, raises++);
					return true;
				})
				.Is(value =>
				{
					Assert.Equal(1, raises++);
					return true;
				})
				.Is(value =>
				{
					Assert.Equal(2, raises++);
					return false;
				})
				.Is(() =>
				{
					Assert.False(true);
					return false;
				})
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);
			
			Assert.Equal(3, raises);
		}
	}
}