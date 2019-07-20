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
			var message = validationContext.When("key", true)
				.IsTrue()
				.AddError("template1");

			Assert.NotNull(message);

			Assert.Equal("key", message.ValidationKey);
			Assert.Equal("template1", message.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, message.ValidationSeverity);
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
			var message = validationContext.When("key", false)
				.IsFalse()
				.AddError("template1");

			Assert.NotNull(message);

			Assert.Equal("key", message.ValidationKey);
			Assert.Equal("template1", message.ValidationMessage);
			Assert.Equal(ValidationSeverity.Error, message.ValidationSeverity);
		}

		[Fact]
		public void IsFalse_Valid()
		{
			var message = validationContext.When("key", true)
				.IsFalse()
				.AddError("template1");

			Assert.Null(message);
		}
	}
}