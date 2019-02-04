using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationConditionComparableExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationConditionComparableExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddPhemaValidation(configuration =>
					configuration.AddComponent<TestModelValidationComponent>())
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsGreater()
		{
			var (key, message) = validationContext.When("age", 11)
				.IsGreater(10)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsGreater_Valid()
		{
			validationContext.When("age", 9)
				.IsGreater(10)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsLess()
		{
			var (key, message) = validationContext.When("age", 11)
				.IsLess(12)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsLess_Valid()
		{
			validationContext.When("age", 11)
				.IsLess(10)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsInRange()
		{
			var (key, message) = validationContext.When("age", 11)
				.IsInRange(10, 12)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.Equal("age", key);
			Assert.Equal("template1", message);
		}

		[Fact]
		public void IsInRange_Less_Valid()
		{
			validationContext.When("age", 9)
				.IsInRange(10, 12)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(!validationContext.Errors.Any());
		}

		[Fact]
		public void IsInRange_Greater_Valid()
		{
			validationContext.When("age", 13)
				.IsInRange(10, 12)
				.AddError<TestModelValidationComponent>(c => c.TestModelTemplate1);

			Assert.True(!validationContext.Errors.Any());
		}
	}
}