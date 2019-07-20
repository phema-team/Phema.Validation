using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Conditions;
using Xunit;

namespace Phema.Validation.Tests
{
	public class ValidationPredicateBooleanExtensionsTests
	{
		private readonly IValidationContext validationContext;

		public ValidationPredicateBooleanExtensionsTests()
		{
			validationContext = new ServiceCollection()
				.AddValidation()
				.BuildServiceProvider()
				.GetRequiredService<IValidationContext>();
		}

		[Fact]
		public void IsTrue()
		{
			var error = validationContext.When("key", true)
				.IsTrue()
				.AddError("template1");

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
				.AddError("template1");

			Assert.Null(error);
		}

		[Fact]
		public void IsFalse()
		{
			var error = validationContext.When("key", false)
				.IsFalse()
				.AddError("template1");

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
				.AddError("template1");

			Assert.Null(error);
		}
	}
}