using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionBooleanExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionBooleanExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration => 
					configuration.AddComponent<TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsTrue()
		{
			var error = validationContext.When("key", true)
				.IsTrue()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void IsTrue_Valid()
		{
			var error = validationContext.When("key", false)
				.IsTrue()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Null(error);
		}

		[Fact]
		public void IsFalse()
		{
			var error = validationContext.When("key", false)
				.IsFalse()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.NotNull(error);

			Assert.Equal("key", error.Key);
			Assert.Equal("template1", error.Message);
			Assert.Equal(ValidationSeverity.Error, error.Severity);
		}

		[Fact]
		public void IsFalse_Valid()
		{
			var error = validationContext.When("key", true)
				.IsFalse()
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Null(error);
		}
	}
}